using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Gunner : Zombie
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunPoint;
    private Vector2 defaultPos = Vector2.zero;
    private Vector2 defaultPointPos = Vector2.zero;
    private SpriteRenderer gunSR;

    new private void Awake()
    {
        base.Awake();
        gunSR = gun.GetComponent<SpriteRenderer>();
        defaultPos = gun.transform.position;
        defaultPointPos = gunPoint.transform.position;
    }

    void Update()
    {
        if (sr.flipX)
        {
            gun.transform.localPosition = defaultPos * new Vector2(-1, 1);
            gunPoint.transform.localPosition = defaultPointPos * new Vector2(-1, 1);

            gunSR.flipX = false;
        }
        else
        {
            gunPoint.transform.localPosition = defaultPointPos;
            gun.transform.localPosition = defaultPos;
            gunSR.flipX = true;
        }


        // Get the mouse position in world space
        Vector3 playerPos = player.transform.position;
        playerPos.z = 0; // Set Z to 0 if you're working in a 2D space

        // Calculate the direction from the object to the mouse
        Vector3 direction = playerPos - transform.position;

        // Calculate the angle in degrees and rotate the object to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Rotate away from the mouse if the sprite is flipped
        if (sr.flipX)
        {
            angle += 180;
        }

        // Apply the rotation
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
