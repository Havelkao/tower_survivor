using System;
using System.Collections.Generic;

public class GlobalWeaponModifiers : IUpgradable
{
    public static Dictionary<DamageType, DamageTypeMulti> damageTypesMultipliers = new() {
        { DamageType.Normal, new DamageTypeMulti(0, 0f, 0f) },
        { DamageType.Magic, new DamageTypeMulti(0, 0f, 0f) },
        { DamageType.Pierce, new DamageTypeMulti(0, 0f, 0f) },
        { DamageType.Chaos, new DamageTypeMulti(0, 0f, 0f) }
    };

    private static Dictionary<DamageType, DamageTypeMulti> defaultState = new(damageTypesMultipliers);

    public static void Reset()
    {
        damageTypesMultipliers = defaultState;
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        //switch (upgrade.property)
        //{
        //case WeaponUpgradableProp.baseDamage:
        //    weapon.baseDamage += (int)value;
        //    break;
        //case WeaponUpgradableProp.damageMultiplier:
        //    weapon.damage += (int)value;
        //    break;
        //case WeaponUpgradableProp.attackSpeedMultiplier:
        //    weapon.attackSpeedMultiplier += value;
        //    break;
        //}
    }
}

public class GlobalEnemyModifiers : IUpgradable
{
    public static float damageMulti = 0f;
    public static float healthMulti = 0f;
    public static float attackSpeedMulti = 0f;
    public static float movementSpeedMulti = 0f;
    public static int armour = 0;

    public static void Reset()
    {
        damageMulti = 0f; healthMulti = 0f; attackSpeedMulti = 0f; movementSpeedMulti = 0f; armour = 0;
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.DamageMulti:
                damageMulti += upgrade.value;
                break;
            case UpgradableProp.AttackSpeedMulti:
                attackSpeedMulti += upgrade.value;
                break;
            case UpgradableProp.HealthMulti:
                healthMulti += upgrade.value;
                break;
            case UpgradableProp.MovementSpeedMulti:
                movementSpeedMulti += upgrade.value;
                break;
        }
    }
}

public class Utils
{
    private static float[,] DamageMatrix =
    {
        { 1.00f, 1.00f, 1.50f, 1.00f },
        { 1.50f, 2.00f, 0.75f, 1.00f },
        { 0.75f, 1.25f, 0.75f, 2.00f },
        { 1.00f, 1.00f, 1.00f, 1.00f }
    };

    public static float GetTypeMulti(DamageType damageType, ArmourType armourType)
    {
        return DamageMatrix[(int)damageType, (int)armourType];
    }

    public static float CalculateArmourEffect(float armor)
    {
        if (armor > 0)
        {
            return 1 - (armor * 0.06f) / (1 + 0.06f * armor);
        }
        else
        {
            return 2 - (float)Math.Pow(0.94, -armor);
        }
    }
};




