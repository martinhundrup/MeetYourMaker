using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/WeaponData")]
public class WeaponData : ItemData
{
    public enum WeaponType 
    {
         Blunt, // higher damage against rocks
         Blade, // higher damage to enemies
         Axe, // higher damage to flora
         Spear, // higher range/attack speed
         None, // nothing extra
    }

    [SerializeField] private WeaponType type;

    [SerializeField, Tooltip("The effect to play when attacking.")] 
    private RuntimeAnimatorController effectAnimator;

    // The hitbox of the weapon.
    [SerializeField, Tooltip("The hitbox of the weapon.")]
    private GameObject hitbox;

    [SerializeField, Tooltip("The damage dealt.")] 
    private float damage;

    [SerializeField, Tooltip("The time waited after the end of the previous " +
        "attack before the next attack can be inititated.")] 
    private float cooldown;

    [SerializeField, Tooltip("The time the hitbox is active.")] 
    private float duration;

    // The force applied to enemies that get knocked back.
    [SerializeField, Tooltip("The knockback applied to enemies.")] 
    private float knockback;

    // The amount of time enemies are prevented from moving.
    [SerializeField, Tooltip("The time enemies are stunned after hit.")] 
    private float stunTime;

    #region PROPERTIES

    public WeaponType Type
    {
        get { return type; }
    }

    public RuntimeAnimatorController EffectAnimator
    {
        get { return effectAnimator; }
    }

    public GameObject Hitbox
    {
        get { return hitbox; }
    }

    public float Damage
    {
        get { return damage; }
    }

    public float Cooldown
    {
        get { return cooldown; }
    }

    public float Duration
    {
        get { return duration; }
    }

    public float Knockback
    {
        get { return knockback; }
    }

    public float StunTime
    {
        get { return stunTime; }
    }

    #endregion

}