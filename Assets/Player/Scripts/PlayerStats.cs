using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public delegate void DamageTakenAction();
    public event DamageTakenAction OnDamageTaken;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerMaxHealth;
    [SerializeField] private float playerHealth; // current health
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDuration;
    [SerializeField] private WeaponData equipedWeapon;

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

    public float PlayerMaxHealth
    {
        get { return playerMaxHealth; }
    }
    public float PlayerHealth
    {
        get { return playerHealth; }
        set 
        { 
            playerHealth = value; 
            playerHealth = Mathf.Clamp(playerHealth, 0f, playerMaxHealth);
            if (OnDamageTaken != null)
                OnDamageTaken();
        }
    }
    public WeaponData EquipedWeapon 
    {
        get { return equipedWeapon; }
    }

    #endregion
}
