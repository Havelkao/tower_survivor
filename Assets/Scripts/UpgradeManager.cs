using System.Collections.Generic;
using UnityEngine;
using static Types;

public class UpgradeManager : MonoBehaviour
{
    public Upgrade upgrade;

    void Awake()
    {
        GetSubject();
    }

    public void Buy()
    {
        if (Player.Instance.bank > upgrade.price)
        {
            Player.Instance.ChangeBankValue(-upgrade.price);
            Apply(upgrade.subjectInstance);
        }        
    }    

    private void GetSubject()
    {

        switch (upgrade.subject)
        {
            case UpgradeSubject.Player:
                upgrade.subjectInstance = Player.Instance;
                break;
            case UpgradeSubject.Enemy:
                upgrade.subjectInstance = new GlobalEnemyModifiers();
                break;
            case UpgradeSubject.Weapon:
                List<RangedWeapon> weapons = GameObject.Find("Tower").GetComponent<Shooter>().weapons;
                print(weapons.Count);
                
                foreach (Weapon weapon in weapons)
                {
                    print(upgrade.weaponType.ToString());
                    if (weapon.name == upgrade.weaponType.ToString() + "(Clone)")
                    {
                        upgrade.subjectInstance = weapon;
                        break;
                    }
                }
                break;
            default:
                print("no subject found");
                break;
        }

    }

    public void Apply(IUpgradable upgradable)
    {
        print("upgradable");
        print(upgradable);
        switch (upgradable)
        {
            case RangedWeapon weapon:
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

    public void Apply(RangedWeapon weapon)
    {
        print("Apply weapon");
        switch (upgrade.property)
        {
            case UpgradableProps.baseDamage:
                weapon.baseDamage += (int) upgrade.value;
                break;
            case UpgradableProps.damageMultiplier:
                weapon.damage += (int)upgrade.value;
                break;
            case UpgradableProps.attackSpeedMultiplier:
                weapon.attackSpeedMultiplier += upgrade.value;
                break;
            case UpgradableProps.aoe:
                weapon.aoe += upgrade.value;
                break;
            case UpgradableProps.chains:
                weapon.damage += (int)upgrade.value;
                break;
            case UpgradableProps.multistrike:
                weapon.multiStrike += (int) upgrade.value;
                print("Apply ms");

                break;
        }
    }

    public void Apply(Player player)
    {
        switch (upgrade.property)
        {
            case UpgradableProps.baseDamage:
                player.IncreaseIncome((int )upgrade.value);
                break;
            case UpgradableProps.health:
                player.health += upgrade.value;
                break;
            case UpgradableProps.armour:
                player.armour += (int)upgrade.value;
                break;
        }
    }

    public void Apply(Enemy enemy)
    {
        switch (upgrade.property)
        {
            case UpgradableProps.baseDamage:
                enemy.baseDamage += (int)upgrade.value;
                break;
            case UpgradableProps.health:
                enemy.health += upgrade.value;
                break;
            case UpgradableProps.armour:
                enemy.armour += (int)upgrade.value;
                break;
            case UpgradableProps.attackSpeedMultiplier:
                enemy.attackSpeedMultiplier += upgrade.value;
                break;
        }
    }
}
