using System;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : ScriptableObject
{
    public int baseDamage;
    public int damage;
    public float baseAttackSpeed;
    public float attackSpeed;
    public Types.DamageType damageType;
    public float range;
    public bool isGroundWeapon;
    public float aoe;

    private float damageMultiplier;
    private float damageTypeMultiplier;
    private float attackSpeedMultiplier;

    private void Awake()
    {
        
    }
    public void Upgrade(WeaponUpgrade upgrade)
    {

    }

}

public class DamageTypeMultipliers
{
    public DamageTypeMultipliers()
    {

    }
}

public class Upgrade : ScriptableObject
{
    public Upgradable property;
    public UpgradeType type;
    public Image sprite;
    public float value;
    public int weight;

    public void ApplyUpgrade()
    {
        //weapon.Upgrade(this);
    }
}

public class WeaponUpgrade : Upgrade
{
    

    

}


public enum Upgradable
{
    baseAttackSpeed,
    attackSpeedMultiplier,
    baseDamage,
    damageMultiplier,
    aoe,
    chains,
    income,
    bounty
}

public enum UpgradeType
{
    weapon,
    player,
    enemy
}



//public void BuyMultistrike()
//{
//    int price = 100;
//    if (Player.Instance.bank >= price)
//    {
//        Player.Instance.ChangeBankValue(-price);
//        weapon.multiStrike += 1;
//    }
//}