using System.Text.RegularExpressions;
using UnityEngine;

public enum ArmourType
{
    Unarmored = 0,
    Light = 1,
    Medium = 2,
    Heavy = 3
}

public enum DamageType
{
    Normal = 0,
    Pierce = 1,
    Magic = 2,
    Chaos = 3
};

public enum TargetMode
{
    Oldest,
    Newest,
    Random,
    Closest
}

public enum PhysicsLayer
{
    Projectile = 3,
    Enemy = 6
}

public enum UpgradableProp
{
    BaseDamage,
    DamageMulti,
    AttackSpeedMulti,
    ProjectileCount,
    Aoe,
    ChainCount,
    Armour,
    MaxHealth,
    MovementSpeedMulti,
    HealthMulti,
    Bounty,
    Income
}

public static class EnumExtensions
{
    public static string Textify(this UpgradableProp prop)
    {
        return prop.ToString()[0] + Regex.Replace(prop.ToString().Substring(1), @"([A-Z])", " $1");
    }
}

public enum UpgradeSubject
{
    Weapon,
    Player,
    Enemy
}

public enum WeaponType
{
    Bow,
    Cannon,
    None
}

[System.Serializable]
public struct WaveEnemy
{
    public GameObject prefab;
    public float spawnRate;
}

public interface IUpgradable
{
    void ApplyUpgrade(Upgrade upgrade);
}

public struct DamageTypeMulti
{
    public int baseDamage;
    public float damageMulti;
    public float attackSpeedMulti;

    public DamageTypeMulti(int baseDamage, float damageMulti, float attackSpeedMulti)
    {
        this.baseDamage = baseDamage;
        this.damageMulti = damageMulti;
        this.attackSpeedMulti = attackSpeedMulti;
    }
}