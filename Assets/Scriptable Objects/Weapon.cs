using System.Collections.Generic;
using UnityEngine;
using static Types;

public class Weapon : ScriptableObject, IUpgradable
{
    public int baseDamage;
    public int damage;
    public float baseAttackSpeed;
    public float attackSpeed;
    public DamageType damageType;
    public float range;
    public bool isGroundWeapon;
    public float aoe;
    public float damageMultiplier;
    public float damageTypeMultiplier;
    public float attackSpeedMultiplier;

    void ApplyUpgrade()
    {

    }
}

public struct DamageTypeMultipliers
{
    public int baseDamage;
    public float damageMulti;
    public float attackSpeedMulti;

    public DamageTypeMultipliers(int baseDamage, float damageMulti, float attackSpeedMulti)
    {
        this.baseDamage = baseDamage;
        this.damageMulti = damageMulti;
        this.attackSpeedMulti = attackSpeedMulti;
    }
}

public class GlobalWeaponModifiers : IUpgradable
{
    public static Dictionary<DamageType, DamageTypeMultipliers> damageTypesMultipliers = new() {
        { DamageType.Normal, new DamageTypeMultipliers(0, 0f, 0) },
        { DamageType.Magic, new DamageTypeMultipliers(0, 0f, 0) },
        { DamageType.Pierce, new DamageTypeMultipliers(0, 0f, 0) },
        { DamageType.Chaos, new DamageTypeMultipliers(0, 0f, 0) }
    };

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            //case WeaponUpgradableProp.baseDamage:
            //    weapon.baseDamage += (int)value;
            //    break;
            //case WeaponUpgradableProp.damageMultiplier:
            //    weapon.damage += (int)value;
            //    break;
            //case WeaponUpgradableProp.attackSpeedMultiplier:
            //    weapon.attackSpeedMultiplier += value;
            //    break;
        }
    }
}

public class GlobalEnemyModifiers : IUpgradable
{
    public static float damageMulti;
    public static float healthMulti;
    public static float attackSpeedMulti;
    public static float movementSpeedMulti;
    public static int armour;

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.damageMulti:
                damageMulti += upgrade.value;
                break;
            case UpgradableProp.attackSpeedMulti:
                attackSpeedMulti += upgrade.value;
                break;
            case UpgradableProp.healthMulti:
                healthMulti += upgrade.value;
                break;
            case UpgradableProp.movementSpeedMulti:
                movementSpeedMulti += upgrade.value;
                break;
        }
    }
}

