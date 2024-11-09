using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public delegate void DamageTakenAction();
    public event DamageTakenAction OnDamageTaken;

    [Header("Player stuff")]
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float playerMaxHealth = 5;
    [SerializeField] private float playerHealth = 5; // current health
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private int maxAmmo = 10;

    [Header("Roll stuff")]
    [SerializeField] private bool hasRoll = false;
    [SerializeField] private float rollSpeed = 8;
    [SerializeField] private float rollDuration = 0.4f;

    [Header("Crouch Stuff")]
    [SerializeField] private bool hasCrouch = false;
    [SerializeField] private float crouchRegeneration = 0f;

    [Header("Gun Stats")]
    [SerializeField] private bool canShoot = false;
    [SerializeField] private int bulletCount = 4;
    [SerializeField] private float bulletSpread = 10;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float reloadTime = 1f;

    [Header("Bullet Stats")]
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private float bulletSize = 1f;
    [SerializeField] private float knockbackForce = 0f;
    [SerializeField] private float stunTime = 0f;
    [SerializeField] private bool piercing = false;
    [SerializeField] private bool bounces = false;

    #region PROPERTIES
    public float RollDuration
    {
        get { return rollDuration; }
    }

    public float RollSpeed
    {
        get { return rollSpeed; }
    }

    public float PlayerSpeed
    {
        get { return playerSpeed; }
    }

    public bool CanShoot
    {
        get { return canShoot; }
        set { canShoot = value; }
    }

    public float PlayerMaxHealth
    {
        get { return playerMaxHealth; }
    }

    public float PlayerHealth
    {
        get { return playerHealth; }
        set
        {
            playerHealth = Mathf.Clamp(value, 0f, playerMaxHealth);
            if (OnDamageTaken != null)
                OnDamageTaken();
        }
    }

    public int AmmoCount
    {
        get { return ammoCount; }
        set { ammoCount = value; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
    }

    public bool HasRoll
    {
        get { return hasRoll; }
    }

    public bool HasCrouch
    {
        get { return hasCrouch; }
    }

    public float CrouchRegeneration
    {
        get { return crouchRegeneration; }
    }

    public int BulletCount
    {
        get { return bulletCount; }
        set { bulletCount = value; }
    }

    public float BulletSpread
    {
        get { return bulletSpread; }
        set { bulletSpread = value; }
    }

    public GameObject Bullet
    {
        get { return bullet; }
    }

    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }

    public float ReloadTime
    {
        get { return reloadTime; }
        set { reloadTime = value; }
    }

    public float BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }

    public float BulletSize
    {
        get { return bulletSize; }
        set { bulletSize = value; }
    }

    public bool Piercing
    {
        get { return piercing; }
        set { piercing = value; }
    }

    public bool Bounces
    {
        get { return bounces; }
        set { bounces = value; }
    }

    public float KnockbackForce
    {
        get { return knockbackForce; }
        set { knockbackForce = value; }
    }

    public float StunTime
    {
        get { return stunTime; }
        set {  stunTime = value; }
    }

    #endregion
}
