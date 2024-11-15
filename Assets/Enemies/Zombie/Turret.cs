using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform spawn;       // the spawn point of the bullet
    [SerializeField] private GameObject projectile; // the projectile to shoot
    [SerializeField] private bool targetsPlayer;    // aims at player
    [SerializeField] private bool cardinal;         // aims in the four cardinal directions (N, S, E, W)
    [SerializeField] private bool ordinal;          // aims in the four ordinal directions (NW, SW, NE, SE)
    [SerializeField] private bool spins;            // moves between each of the 8 directions
    [SerializeField] private bool alternates;       // switches between ordinal and cardinal every shot
    [SerializeField] private float totalAngle = 30f;// Total range between shots
    private int spinIndex = 0; // keeps track of number of spins

    [SerializeField] int projectilesPerShot = 1;    // the number of projectiles shot when firing
    [SerializeField] float fireRate;                // the time in between each shot

    private PlayerController player; // ref to the player object

    private static readonly List<Vector2> directions = new List<Vector2> // the list of spin directions
    {
        new Vector2(0, 1),   // North
        new Vector2(1, 1),   // North-East
        new Vector2(1, 0),   // East
        new Vector2(1, -1),  // South-East
        new Vector2(0, -1),  // South
        new Vector2(-1, -1), // South-West
        new Vector2(-1, 0),  // West
        new Vector2(-1, 1)   // North-West
    };

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(Timer());

        if (alternates)
        {
            cardinal = true;
            ordinal = false;
        }
    }

    // runs the timer and calls the fire function.
    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
        }
    }

    // finds the direction to fire
    private void Fire()
    {
        if (targetsPlayer)
        {
            this.Shoot(player.transform.position - spawn.position);
        }
        if (cardinal) // shoots up, right, left, down
        {
            // shoot up
            this.Shoot(Vector2.up);

            // shoot left
            this.Shoot(Vector2.left);

            // shoot right
            this.Shoot(Vector2.right);

            // shoot down
            this.Shoot(Vector2.down);
        }
        if (ordinal)
        {
            // shoot up right
            this.Shoot(Vector2.up + Vector2.right);

            // shoot up left
            this.Shoot(Vector2.up + Vector2.left);

            // shoot down right
            this.Shoot(Vector2.down + Vector2.right);

            // shoot down left
            this.Shoot(Vector2.down + Vector2.left);
        }
        if (spins)
        {
            this.Shoot(FindNextSpinDir());
        }
        if (alternates)
        {
            cardinal = !cardinal;
            ordinal = !ordinal;
        }
    }

    // shoots the appropriate amount of bullets in their respective directions.
    private void Shoot(Vector2 _dir)
    {
        SFXManager.instance.PlayEnemyFire();
        if (projectilesPerShot == 1)
        {
            GameObject _p = Instantiate(projectile);
            _p.transform.position = spawn.position;
            _p.GetComponent<Bullet>().Initialize(spawn.position, _dir, false);
            return;
        }

        float angleIncrement = totalAngle / (projectilesPerShot - 1);
        float baseAngle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < projectilesPerShot; i++)
        {
            float currentAngle = baseAngle - (totalAngle / 2f) + (i * angleIncrement);
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector2 newDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            GameObject _p = Instantiate(projectile);
            _p.transform.position = this.transform.position;
            _p.GetComponent<Bullet>().Initialize(spawn.position, newDirection, false);
        }
    }

    private Vector2 FindNextSpinDir()
    {
        var direction = directions[spinIndex];
        spinIndex = (spinIndex + 1) % directions.Count;
        return direction;
    }
}