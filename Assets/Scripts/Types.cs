using UnityEngine;

public class Types
{
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
        baseDamage,
        damageMulti,
        attackSpeedMulti,
        multistrike,
        aoe,
        chains,
        income,
        bounty,
        armour,
        health,
        movementSpeedMulti,
        healthMulti
    }

    public enum WeaponUpgradableProp
    {
        baseDamage,
        damageMulti,
        attackSpeedMulti,
        multistrike,
        aoe,
        chainCount
    }

    public enum PlayerUpgradableProp
    {
        income, 
        bounty, 
        armour, 
        maxHealth
    }

    public enum EnemyUpgradableProp
    {
        damageMulti = UpgradableProp.damageMulti, 
        attackSpeedMulti = UpgradableProp.attackSpeedMulti,
        movementSpeedMulti = UpgradableProp.movementSpeedMulti
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
        Cannon
    }


    //public struct DamageTypeMultiplier
    //{
    //    public DamageType damageType;
    //    public float multiplier;
    //}

    [System.Serializable]
    public struct WaveEnemy
    {
        public GameObject prefab;
        public float spawnRate;
    }

    public interface IUpgradable
    {
        //void ApplyUpgrade(UpgradableProps property, float value);
    }
}
