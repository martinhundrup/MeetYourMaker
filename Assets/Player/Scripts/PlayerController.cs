using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private WeaponSprite weaponSprite;
    private Vector2 facingDirection; // the direction of the cursor relative to shroomie

    private PlayerStats playerStats;
    private Blink blink;
    private bool isInvulnerable;
    private bool isInCooldown;
    private bool acceptingInput = true;
    private float inputX;
    private float inputY;
    private bool crouched = false;
    private bool rolling = false;
    [SerializeField] private GameObject dust;
    private GameObject bulletSpawner;
    [SerializeField] private GameObject reloadIndicator;
    [SerializeField] private GameObject reloadProgress;

    private void Awake()
    {
        reloadIndicator.SetActive(false);
        playerStats = DataDictionary.PlayerStats;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        blink = GetComponentInChildren<Blink>();
        animator = GetComponentInChildren<Animator>();
        weaponSprite = GetComponentInChildren<WeaponSprite>();
        GameEvents.OnGamePaused += SetAcceptingInput;
        // for now, heal player on awake
        playerStats.PlayerHealth = playerStats.PlayerMaxHealth;
        bulletSpawner = GameObject.FindGameObjectWithTag("BulletSpawner");
        StartCoroutine(DustSpawner());
    }

    private void OnDestroy()
    {
        GameEvents.OnGamePaused -= SetAcceptingInput;
    }

    // get input for movement
    private void Update()
    {
        Movement();
        FindFacingDirection();        
        Attack();
        Crouch();
        Roll();

#if UNITY_EDITOR
        Debug.DrawLine(bulletSpawner.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), UnityEngine.Color.red);
#endif
    }

    private void SetAcceptingInput(bool _input)
    {
        acceptingInput = !_input;
    }

    private void Attack()
    {
        if (rolling || crouched) return;

        // shooting
        if (Input.GetButton("Fire") && playerStats.AmmoCount > 0 && !isInCooldown)
        {
            SFXManager.instance.PlayFire();

            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set Z to 0 if you're working in a 2D space

            // Calculate the direction from the object to the mouse
            Vector3 direction = mousePosition - transform.position;

            for (int i = 0; i < playerStats.BulletCount; i++)
            {
                var obj = Instantiate(playerStats.Bullet);
                obj.GetComponent<Bullet>().Initialize(bulletSpawner.transform.position, direction, playerStats.BulletSpread, playerStats.BulletSpeed);
            }

            playerStats.AmmoCount--;
            StartCoroutine(CooldownTimer(playerStats.RecoilTime));
        }
    }

    private void Movement()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if (rolling) return;

        var movementVect = new Vector2(inputX, inputY).normalized * playerStats.PlayerSpeed;

        if (!acceptingInput || crouched) // disable player movement
            movementVect = Vector2.zero;

        if (movementVect.magnitude > 0.01)
        {
            animator.Play("run");
        }
        else if (!crouched)
        {
            animator.Play("idle");
        }

        rb.velocity = movementVect;
    }

    private void FindFacingDirection()
    {
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        facingDirection = newPos.normalized;

        bool flipX = true;

        // set sprite flipped on x or not depending on facing direction
        if (facingDirection.x <= 0) flipX = false;

        sr.flipX = flipX;
        weaponSprite.SpriteFlipX(flipX);
    }

    private void Crouch()
    {
        if (rolling) return;
        if (acceptingInput && Input.GetButtonDown("Crouch"))
        {
            crouched = true;
            animator.Play("root");
            weaponSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (acceptingInput && Input.GetButtonUp("Crouch"))
        {
            crouched = false;
            animator.Play("uproot");
            weaponSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    private void Roll()
    {
        if (!rolling && acceptingInput && Input.GetButtonDown("Roll") && !crouched && !(inputX == 0f && inputY == 0f))
        {
            Debug.Log("roll");
            StartCoroutine(_Roll());
        }
    }

    private IEnumerator _Roll()
    {
        rolling = true;
        animator.Play("root");
        rb.velocity = Vector2.zero;
        weaponSprite.GetComponent<SpriteRenderer>().enabled = false;
        isInvulnerable = true;
        //rb.velocity = Vector3.zero;
        var movementVect = new Vector2(inputX, inputY).normalized * playerStats.RollSpeed;

        yield return new WaitForSeconds(0.2f); // wait for animtion to play before invisible
        sr.enabled = false;
        gameObject.layer = 6; // roll layer

        rb.velocity = movementVect;
        yield return new WaitForSeconds(playerStats.RollDuration);

        //rb.velocity = Vector3.zero;
        sr.enabled = true;
        isInvulnerable = false;
        animator.Play("uproot");
        rb.velocity = Vector2.zero;
        gameObject.layer = 0; // default
        weaponSprite.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f); // wait for animtion to play before invisible

        rolling = false;
    }

    // detect collision from enemy bodies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && !isInvulnerable)
        {
            playerStats.PlayerHealth -= enemy.ContactDamage;
            StartCoroutine(MakeInvulnerable(0.4f));
            if (playerStats.PlayerHealth == 0)
            {
                Debug.LogError("Shroomie died!");
            }
        }        
    }

    private IEnumerator MakeInvulnerable(float _time)
    {
        isInvulnerable = true;
        blink.StartBlinking();

        yield return new WaitForSeconds(0.4f);

        isInvulnerable = false;
        blink.StopBlinking();
    }

    // reload
    private IEnumerator CooldownTimer(float _time)
    {
        isInCooldown = true;
        reloadIndicator.SetActive(true);

        float remainingTime = 0;
        while (remainingTime < _time)
        {
            remainingTime += Time.deltaTime;
            reloadProgress.transform.localScale = new Vector2(remainingTime / _time, 1f);
            yield return null;
        }

        isInCooldown = false;
        reloadIndicator.SetActive(false);
    }

    private IEnumerator DustSpawner()
    {
        SpriteRenderer dustSR = null;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (dustSR != null) Destroy(dustSR.gameObject);
            if ((inputX != 0f || inputY != 0f) &&
                (rb.velocity.x != 0f || rb.velocity.y != 0f))
            {             
                dustSR = Instantiate(dust).GetComponent<SpriteRenderer>();
                if (rb.velocity.x > 0f) dustSR.flipX = true;
                dustSR.transform.position = transform.position;
            }
        }
    }
}
