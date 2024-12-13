using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Types;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradableProp property;
    public UpgradeSubject subject;
    public IUpgradable subjectInstance;
    public Image sprite;
    public float value;
    public int price;
    public int weight;
    public WeaponType weaponType;
    public DamageType damageType;

    void Awake()
    {
        GetSubject();
    }

    public void Buy()
    {
        if (Player.Instance.bank > price)
        {
            Player.Instance.ChangeBankValue(-price);
            subjectInstance.ApplyUpgrade(this);
            Apply(subjectInstance);
        }
    }

    private void GetSubject()
    {        

        switch (subject)
        {
            case UpgradeSubject.Player:
                subjectInstance = Player.Instance;
                break;
            case UpgradeSubject.Enemy:
                subjectInstance = new GlobalEnemyModifiers();
                break;
            case UpgradeSubject.Weapon:
                List<RangedWeapon> weapons = GameObject.Find("Tower").GetComponent<Shooter>().weapons;
                foreach (Weapon weapon in weapons)
                {
                    if (weapon.name == WeaponType.Cannon.ToString() + "(Clone)")
                    {
                        subjectInstance = weapon;
                        break;
                    } 
                }
                break;
        }

    }

    public void Apply(IUpgradable upgradable)
    {
        switch (upgradable)
        {
            case Weapon weapon:
                Apply(weapon);
                break;
            case Player player:
                Apply(player);
                break;
            case Enemy enemy:
                Apply(enemy);
                break;
        }
    }

    public void Apply(Weapon weapon)
    {
        switch (property)
        {
            case UpgradableProps.baseDamage:
                weapon.baseDamage += (int)value;
                break;
            case UpgradableProps.damageMultiplier:
                weapon.damage += (int)value;
                break;
            case UpgradableProps.attackSpeedMultiplier:
                weapon.attackSpeedMultiplier += value;
                break;
            case UpgradableProps.aoe:
                weapon.aoe += value;
                break;
            case UpgradableProps.chains:
                weapon.damage += (int)value;
                break;
        }
    }

    public void Apply(Player player)
    {
        switch (property)
        {
            case UpgradableProps.baseDamage:
                player.IncreaseIncome((int)value);
                break;
            case UpgradableProps.health:
                player.health += value;
                break;
            case UpgradableProps.armour:
                player.armour += (int)value;
                break;
        }
    }

    public void Apply(Enemy enemy)
    {
        switch (property)
        {
            case UpgradableProps.baseDamage:
                enemy.baseDamage += (int)value;
                break;
            case UpgradableProps.health:
                enemy.health += value;
                break;
            case UpgradableProps.armour:
                enemy.armour += (int)value;
                break;
            case UpgradableProps.attackSpeedMultiplier:
                enemy.attackSpeedMultiplier += value;
                break;
        }
    }
}

//public class WeaponUpgrade : Upgrade
//{
//    public WeaponType weapon;

//}