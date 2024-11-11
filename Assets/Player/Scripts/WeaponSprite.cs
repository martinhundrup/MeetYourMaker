using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer sr;
    private Vector2 defaultPos;
    private Vector2 defaultPointPos;
    [SerializeField] private GameObject point;
    [SerializeField] private bool targetsPlayer = false; // is this an enemy weapon?
    [SerializeField] private SpriteRenderer parentRenderer;

    private void Awake()
    {
        defaultPos = transform.localPosition;
        defaultPointPos = point.transform.localPosition;
        player = FindObjectOfType<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void SpriteFlipX(bool flip)
    {        
        if (flip)
        {
            transform.localPosition = defaultPos * new Vector2(-1, 1);
            point.transform.localPosition = defaultPointPos * new Vector2(-1, 1);

            sr.flipX = false;
        }
        else
        {
            point.transform.localPosition = defaultPointPos;
            transform.localPosition = defaultPos;
            sr.flipX = true;
        }
    }

    private void Update()
    {
        if (!targetsPlayer && DataDictionary.PlayerStats.PlayerHealth <= 0 || CentralGameManager.Paused) return;

        if (targetsPlayer)
        {
            SpriteFlipX(parentRenderer.flipX);
        }
            

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (targetsPlayer) mousePosition = player.transform.position;
        mousePosition.z = 0; // Set Z to 0 if you're working in a 2D space

        // Calculate the direction from the object to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees and rotate the object to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Rotate away from the mouse if the sprite is flipped
        if (sr.flipX)
        {
            angle += 180;
        }

        // Apply the rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
