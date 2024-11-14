using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System.Runtime.InteropServices;
using AOT;


[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    private struct SavableData
    {
        public float playerSpeed;
        public float maxHealth;
        public int maxAmmo;
        public int exp;
        public int deaths;
        public float gameTime;
        public bool hasRoll;
        public bool hasCrouch;
        public float crouchRegen;
        public int bulletCount;
        public float bulletSpread;
        public float bulletSpeed;
        public float reloadTime;
        public float bulletSize;
        public float bulletDamage;
        public float knockback;
        public float stun;
        public bool pierces;
    }

    private static PlayerStats instance;


    public delegate void DamageTakenAction();
    public event DamageTakenAction OnDamageTaken;

    [Header("Player stuff")]
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float playerMaxHealth = 5;
    [SerializeField] private float playerHealth = 5; // current health
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int exp = 0;
    [SerializeField] private int deaths = 0;
    [SerializeField] private float gameTime = 0;

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
    
    public float GameTime
    {
        get { return gameTime; }
        set { gameTime = value; }
    }
    public int Deaths
    {
        get { return deaths; }
        set { deaths = value; }
    }
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

    public bool UsesAmmo
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

    private void OnEnable()
    {
        // Set the instance when this scriptable object is enabled
        instance = this;
    }


    [Button]

    // sets values to what they should be on respawn
    public void Respawn()
    {
        playerHealth = playerMaxHealth;
        ammoCount = 30;
        //exp = 0; // don't reset exp
        OnDamageTaken?.Invoke();
    }

    [Button]
    public void ResetDefaults()
    {
        gameTime = 0;
        deaths = 0;
        playerSpeed = 5f;
        playerMaxHealth = 5f;
        playerHealth = 5f;
        ammoCount = 30;
        maxAmmo = 50;
        exp = 0;
        hasRoll = false;
        rollSpeed = 8f;
        rollDuration = 0.3f;
        hasCrouch = false;
        crouchRegeneration = 0.1f; // 10% max health regen a sec
        canShoot = true;
        bulletCount = 5;
        bulletSpread = 10f;
        bulletSpeed = 10f;
        reloadTime = 1f;
        bulletDamage = .3f;
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
        bulletSpread += mod.bulletSpread;
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


    [DllImport("__Internal")]
    private static extern void SaveFile(string data, string filename);

    public void Save()
    {
        SavableData data = new SavableData
        {
            playerSpeed = playerSpeed,
            maxHealth = playerMaxHealth,
            maxAmmo = maxAmmo,
            exp = exp,
            deaths = deaths,
            gameTime = gameTime,
            hasRoll = hasRoll,
            hasCrouch = hasCrouch,
            crouchRegen = crouchRegeneration,
            bulletCount = bulletCount,
            bulletSpread = bulletSpread,
            bulletSpeed = bulletSpeed,
            reloadTime = reloadTime,
            bulletSize = bulletSize,
            bulletDamage = bulletDamage,
            knockback = knockbackForce,
            stun = stunTime,
            pierces = piercing
        };

        string json = JsonUtility.ToJson(data, true);

#if UNITY_WEBGL && !UNITY_EDITOR
        SaveFile(json, "shroomie_stats.json");
#else
        // For testing in the editor, you could write to Application.persistentDataPath instead.
        Debug.Log("Save feature works in WebGL only.");
#endif
    }

    [DllImport("__Internal")]
    private static extern void UploadFile(System.Action<string> callback);

    public void LoadFromFile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        UploadFile(OnFileUploadedStatic);
#else
        Debug.Log("Load feature works in WebGL only.");
#endif
    }

    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void OnFileUploadedStatic(string json)
    {
        if (instance != null)
        {
            try
            {
                SavableData data = JsonUtility.FromJson<SavableData>(json);

                instance.playerSpeed = data.playerSpeed;
                instance.playerMaxHealth = data.maxHealth;
                instance.maxAmmo = data.maxAmmo;
                instance.exp = data.exp;
                instance.deaths = data.deaths;
                instance.gameTime = data.gameTime;
                instance.hasRoll = data.hasRoll;
                instance.hasCrouch = data.hasCrouch;
                instance.crouchRegeneration = data.crouchRegen;
                instance.bulletCount = data.bulletCount;
                instance.bulletSpread = data.bulletSpread;
                instance.bulletSpeed = data.bulletSpeed;
                instance.reloadTime = data.reloadTime;
                instance.bulletSize = data.bulletSize;
                instance.bulletDamage = data.bulletDamage;
                instance.knockbackForce = data.knockback;
                instance.stunTime = data.stun;
                instance.piercing = data.pierces;

                Debug.Log("Player stats loaded successfully from JSON.");

                // load starting scene
                FindObjectOfType<PauseMenu>().OnKYS();
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading JSON: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("PlayerStats instance not set. Make sure it is enabled before calling LoadFromFile.");
        }
    }
}
