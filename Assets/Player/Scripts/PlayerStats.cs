using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
    [SerializeField] private int exp = 0;

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

    public int EXP
    {
        get { return exp; }
        set { exp = value; }
    }

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
        set 
        { 
            ammoCount = value;
            if (ammoCount > maxAmmo) ammoCount = maxAmmo;
        }
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

    [Button]

    // sets values to what they should be on respawn
    public void Respawn()
    {
        playerHealth = playerMaxHealth;
        ammoCount = 20;
        exp = 0;
        OnDamageTaken?.Invoke();
    }
    public void ResetDefaults()
    {
        playerSpeed = 5f;
        playerMaxHealth = 5f;
        playerHealth = 5f;
        ammoCount = 20;
        maxAmmo = 50;
        exp = 0;
        hasRoll = false;
        rollSpeed = 8f;
        rollDuration = 0.3f;
        hasCrouch = false;
        crouchRegeneration = 0.1f; // 10% max health regen a sec
        canShoot = true;
        bulletCount = 4;
        bulletSpread = 10f;
        bulletSpeed = 10f;
        reloadTime = 1f;
        bulletDamage = 0.2f;
        bulletSize = 1f;
        knockbackForce = 0.1f;
        stunTime = 0.1f;
        piercing = false;
        bounces = false;
    }

    public void ApplyModifier(Modifier mod)
    {
        playerSpeed *= 1f + mod.movementSpeed;
        playerMaxHealth *= 1f + mod.maxHP;
        maxAmmo += mod.maxAmmo;
        bulletSpeed *= 1f + mod.bulletSpeed;
        bulletCount += mod.bulletCount;
        reloadTime *= 1f + mod.reloadTime;
        bulletSpread *= 1f + mod.bulletSpread;
        bulletSize *= 1f + mod.bulletSize;
        bulletDamage *= 1f + mod.bulletDamage;
        if (mod.bulletPiercing) piercing = true;
        if (mod.bulletBouncing) bounces = true;
        knockbackForce *= 1f + mod.bulletKnockback;
        stunTime *= 1f + mod.bulletStun;
        if (mod.crouch) hasCrouch = true;
        crouchRegeneration *= 1f + mod.crouchRegen;
        if (mod.roll) hasRoll = true;
    }
}
