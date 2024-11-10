using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Modifier
{
    public string name;
    public string description;
    public int cost; // amt of exp to buy
    public float movementSpeed; // % change - 100% stays the same
    public float maxHP; // % change
    public int maxAmmo; // addition
    public float bulletSpeed; // % change
    public int bulletCount; // addition
    public float reloadTime; // % change
    public float bulletSpread; // % change
    public float bulletSize; // % change
    public float bulletDamage; // % change
    public bool bulletPiercing; // can only turn on
    public bool bulletBouncing; // can only turn on
    public float bulletKnockback; // % change
    public float bulletStun; // % change
    public bool crouch; // can only turn on
    public float crouchRegen; // % change
    public bool roll; // can only turn on
}

public static class ModifierGenerator
{
    /* list of indices and their corresponding modifier
    // 0 - shroomie movement speed
    // 1 - shroomie max HP
    // 2 - max ammo capacity
    // 3 - bullet speed
    // 4 - bullet count
    // 5 - reload time
    // 6 - bullet spread
    // 7 - bullet size
    // 8 - bullet damage
    // 9 - bullet piercing - 15 pts
    // 10 - bullet bouncing - 15 pts
    // 11 - bullet knockback
    // 12 - bullet stun time
    // 13 - crouch unlock - 20 pts
    // 14 - crouch regen
    // 15 - roll unlock

    // TODO: bomb stuff*/

    // creates a modifier with default values
    public static Modifier GetModifier()
    {
        Modifier mod = new Modifier();
        mod.name = string.Empty;
        mod.description = string.Empty;
        mod.cost = 0;
        mod.movementSpeed = 0;
        mod.maxHP = 0;
        mod.maxAmmo = 0;
        mod.bulletSpeed = 0;
        mod.bulletCount = 0;
        mod.reloadTime = 0;
        mod.bulletSpread = 0;
        mod.bulletSize = 0;
        mod.bulletDamage = 0;
        mod.bulletPiercing = false;
        mod.bulletBouncing = false;
        mod.bulletKnockback = 0;
        mod.bulletStun = 0;
        mod.crouch = false;
        mod.crouchRegen = 0;
        mod.roll = false;

        return mod;
    }

    public static Modifier CreateModifier()
    {
        Modifier mod = GetModifier();      
        int idx = UnityEngine.Random.Range(0, 15);

        // decide what strength
        int strength = 0;
        if (DataDictionary.GameSettings.GameLevel >= 20)
        {
            strength = Random.Range(1, 3); // 1 or 2
        }
        else
        {
            strength = Random.Range(0, 2); // 0 or 1
        }

        mod.cost = (strength + 1) * 15;

        bool neg = false;
        // no more negatives
        //if (Random.Range(0, 4) == 0) neg = true; // make the modifier negative 25% of time

        if (idx == 0) // 0 - shroomie movement speed
        {
            if (strength == 0) mod.movementSpeed = .10f; // 5 % increase
            else if (strength == 1) mod.movementSpeed = .15f; // 10 % increase
            else mod.movementSpeed = .25f; // 20 % increase

            if (neg) mod.movementSpeed *= -1; // make a decrease

            mod.name = "Movement Speed";
            mod.description = $"Increases Shroomie's movement speed by {(1 + mod.movementSpeed) * 100}%.";
        }
        else if (idx == 1) // 1 - shroomie max HP
        {
            if (strength == 0) mod.maxHP = .15f; // 5 % increase
            else if (strength == 1) mod.maxHP = .25f; // 10 % increase
            else mod.maxHP = .40f; // 20 % increase

            if (neg) mod.maxHP *= -1; // make a decrease
            mod.name = "Max Health";
            mod.description = $"Increases Shroomie's max health by {(1 + mod.maxHP) * 100}%.";
        }
        else if (idx == 2) // 2 - max ammo capacity
        {
            if (strength == 0) mod.maxAmmo = 10; // 5 increase
            else if (strength == 1) mod.maxAmmo = 25; // 15 increase
            else mod.maxAmmo = 50; // 25 increase

            if (neg) mod.maxAmmo *= -1; // make a decrease

            mod.name = "Max Ammo";
            mod.description = $"Increases Shroomie's maximum ammo capacity by {mod.maxAmmo}.";
        }
        else if (idx == 3) // 3 - bullet speed
        {
            if (strength == 0) mod.bulletSpeed = .10f; // 5 % increase
            else if (strength == 1) mod.bulletSpeed = .2f; // 10 % increase
            else mod.bulletSpeed = .35f; // 20 % increase

            if (neg) mod.bulletSpeed *= -1; // make a decrease

            mod.name = "Bullet Speed";
            mod.description = $"Increases bullet speed by {(1 + mod.bulletSpeed) * 100}%.";
        }
        else if (idx == 4) // 4 - bullet count
        {
            if (strength == 0) mod.bulletCount = 1; // 1 increase
            else if (strength == 1) mod.bulletCount = 3; // 3 increase
            else mod.bulletCount = 5; // 5 increase

            if (neg) mod.bulletCount *= -1; // make a decrease

            mod.name = "Bullet Count";
            mod.description = $"Increases bullet count per shot by {mod.bulletCount}.";
        }
        else if (idx == 5) // 5 - reload time
        {
            neg = !neg; // invert

            if (strength == 0) mod.reloadTime = .10f; // 5 % increase
            else if (strength == 1) mod.reloadTime = .20f; // 10 % increase
            else mod.reloadTime = .30f; // 20 % increase

            if (neg) mod.reloadTime *= -1; // make a decrease

            mod.name = "Reload Time";
            mod.description = $"Decreases reload time by {(1 + mod.reloadTime) * 100}%.";
        }
        else if (idx == 6) // 6 - bullet spread
        {
            if (strength == 0) mod.bulletSpread = 3; // 1 increase
            else if (strength == 1) mod.bulletSpread = 5; // 3 increase
            else mod.bulletSpread = 10; // 5 increase

            if (neg) mod.bulletSpread *= -1; // make a decrease

            mod.name = "Bullet Spread";
            mod.description = $"Increases bullet spread by {mod.bulletSpread} degrees.";
        }
        else if (idx == 7) // 7 - bullet size
        {
            if (strength == 0) mod.bulletSize = .10f; // 5 % increase
            else if (strength == 1) mod.bulletSize = .15f; // 10 % increase
            else mod.bulletSize = .25f; // 20 % increase

            if (neg) mod.bulletSize *= -1; // make a decrease

            mod.name = "Bullet Size";
            mod.description = $"Increases bullet size by {(1 + mod.bulletSize) * 100}%.";
        }
        else if (idx == 8) // 8 - bullet damage
        {
            if (strength == 0) mod.bulletDamage = .25f; // 25 % increase
            else if (strength == 1) mod.bulletDamage = .5f; // 50 % increase
            else mod.bulletDamage = 1f; // 100 % increase

            if (neg) mod.bulletDamage *= -1; // make a decrease

            mod.name = "Bullet Damage";
            mod.description = $"Increases bullet damage by {(1 + mod.bulletDamage) * 100}%.";
        }
        else if (idx == 9) // 9 - bullet piercing
        {
            mod.bulletPiercing = true;

            mod.name = "Piercing Bullets";
            mod.description = "Grants bullets the ability to pierce through enemies and objects.";
        }
        else if (idx == 10) // 11 - bullet knockback
        {
            if (strength == 0) mod.bulletKnockback = .10f; // 10 % increase
            else if (strength == 1) mod.bulletKnockback = .2f; // 20 % increase
            else mod.bulletKnockback = .3f; // 30 % increase

            if (neg) mod.bulletKnockback *= -1; // make a decrease

            mod.name = "Bullet Knockback";
            mod.description = $"Increases knockback effect of bullets by {(1 + mod.bulletKnockback) * 100}%.";
        }
        else if (idx == 11) // 12 - bullet stun time
        {
            if (strength == 0) mod.bulletStun = .10f; // 10 % increase
            else if (strength == 1) mod.bulletStun = .2f; // 20 % increase
            else mod.bulletStun = .3f; // 30 % increase

            if (neg) mod.bulletStun *= -1; // make a decrease

            mod.name = "Bullet Stun Time";
            mod.description = $"Increases the duration bullets stun enemies by {(1 + mod.bulletStun) * 100}%.";
        }
        else if (idx == 12) // 13 - crouch unlock
        {
            mod.crouch = true;

            mod.name = "Crouch Ability";
            mod.description = "Unlocks the ability for Shroomie to crouch.";
            mod.cost = 50;
        }
        else if (idx == 13) // 14 - crouch regen
        {
            if (!DataDictionary.PlayerStats.HasCrouch) // unlock crouch before offering upgrades
            {
                mod.crouch = true;

                mod.name = "Crouch Ability";
                mod.description = "Unlocks the ability for Shroomie to crouch.";
                mod.cost = 50;
            }
            else
            {
                if (strength == 0) mod.crouchRegen = .10f; // 10 % increase
                else if (strength == 1) mod.crouchRegen = .2f; // 20 % increase
                else mod.crouchRegen = .3f; // 30 % increase

                if (neg) mod.crouchRegen *= -1; // make a decrease

                mod.name = "Crouch Regeneration";
                mod.description = $"Increases health regeneration rate while crouching by {(1 + mod.crouchRegen) * 100}%.";
            }            
        }
        else if (idx == 14) // 15 - roll unlock
        {
            mod.roll = true;

            mod.name = "Roll Ability";
            mod.description = "Unlocks the ability for Shroomie to perform a roll maneuver.";
            mod.cost = 40;
        }

        return mod;
    }

}
