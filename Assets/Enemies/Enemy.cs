using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : Breakable
{
    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied OnEnemyDied;

    [SerializeField] private GameObject corpse; // what to spawn on death
    [SerializeField] private GameObject exp;
    [SerializeField] private float cost; // how many difficulty points the enemy is worth
    protected PlayerController player;
    protected Rigidbody2D rb;
    

    [SerializeField] protected float speed;
    [SerializeField] protected bool hasHitstun = false;
    [SerializeField] protected float contactDamage; // damage dealt to player on contact
    protected bool isInHitstun = false;
    protected bool isInvulnerable = false;
    protected Breakable breakable;
    protected SpriteRenderer sr;
    [SerializeField, Range(0,1)] protected float aggression = 0;
    [SerializeField] private GameObject splatter;

    public float ContactDamage
    {
        get { return contactDamage; }
    }
    public float Cost
    {
        get { return cost; }
    }

    new protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
        breakable = GetComponent<Breakable>();

        base.Awake();
    }

    private void OnDestroy()
    {
        if (Health > 0) return; // deleted on scene end, so don't do anything else
        if (corpse != null) 
        {
            var cor = Instantiate(corpse);
            cor.transform.position = this.transform.position;
            cor.GetComponent<SpriteRenderer>().flipX = sr.flipX;
        }

        // spawn exp - 50% more than cost
        for (int i = 0; i < cost * 1.5f; i++)
        {
            Instantiate(exp).transform.position = this.transform.position;
        }

        if (OnEnemyDied != null) OnEnemyDied(this);
    }

    protected override void TakeDamage(Hitbox _hitbox)
    {
        if (isInvulnerable) return; // don't take damage or knockback or stun
        if (this.CompareTag(_hitbox.HitboxTag)) return; // don't get hit by your own shots
        if (hasHitstun)
            StartCoroutine(HitStun(_hitbox));
        //StartCoroutine(MakeInvulnerable()); // remove invulnerbility so multiple bullets can hit
        base.TakeDamage(_hitbox);
        if (splatter != null)
        {
            var particles = Instantiate(splatter).GetComponent<ParticleSystem>();
            particles.transform.position = this.transform.position;
            particles.Play();
        }
    }

    // Temporarily pauses the enemy when hit.
    protected IEnumerator HitStun(Hitbox _hitbox)
    {
        if (isInHitstun) yield break;
        this.isInHitstun = true;
        var s = this.speed;
        this.speed = 0;
        this.rb.velocity = Vector2.zero;

        yield return new WaitForFixedUpdate(); // add delay for knockback force

        this.rb.AddForce(_hitbox.KnockbackForce * 50 * (this.transform.position - player.transform.position).normalized);

        yield return new WaitForSeconds(_hitbox.StunTime);

        this.speed = s;
        this.isInHitstun = false;
    }

    // makes enemy unable to take more damage when hit
    protected IEnumerator MakeInvulnerable()
    {
        isInvulnerable = true;
        GetComponent<Blink>().StartBlinking();

        yield return new WaitForSeconds(0.4f);

        GetComponent<Blink>().StopBlinking();
        isInvulnerable = false;
    }
}
