using System.Collections.Generic;
using UnityEngine;
using static Types;

public class UpgradeManager : MonoBehaviour
{
    public Upgrade upgrade;
    public IUpgradable subjectInstance;

    void Start()
    {
        //this has to be run be run after shooter init
        // todo fix ?
        GetSubject();
    }

    public void Buy()
    {
        if (Player.Instance.bank > upgrade.price)
        {
            Player.Instance.ChangeBankValue(-upgrade.price);
            subjectInstance.ApplyUpgrade(upgrade);
        }
    }

    private void GetSubject()
    {

        switch (upgrade.subject)
        {
            case UpgradeSubject.Player:
                subjectInstance = Player.Instance;
                break;
            case UpgradeSubject.Enemy:
                subjectInstance = new GlobalEnemyModifiers();
                break;
            case UpgradeSubject.Weapon:
                List<RangedWeapon> weapons = GameObject.Find("Tower").GetComponent<Shooter>().weapons;
                print(weapons.Count);

                foreach (RangedWeapon weapon in weapons)
                {
                    if (weapon.name == upgrade.weaponType.ToString() + "(Clone)")
                    {
                        subjectInstance = weapon;
                    }
                }
                break;
            default:
                throw new System.Exception("No upgrade subject found!");
        }
    }
}
