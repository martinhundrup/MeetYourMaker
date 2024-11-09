using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private string hitboxTag; // breakable objects with a matching tag are friendly
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float stunTime; // amount of time to pause target for

    public void SetStats(WeaponData weaponData)
    {
        damage = weaponData.Damage;
        knockbackForce = weaponData.Knockback;
        stunTime = weaponData.StunTime;
    }

    #region PROPERTIES

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public string HitboxTag
    {
        get { return hitboxTag; }
    }

    public float KnockbackForce
    {
        get { return this.knockbackForce; }
        set { this.knockbackForce = value; }
    }

    public float StunTime
    {
        get { return this.stunTime; }
        set { this.stunTime = value; }
    }
    #endregion
}
