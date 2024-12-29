using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradableProp property;
    public UpgradeSubject subject;
    private IUpgradable subjectInstance;
    public Image sprite;
    public float value;
    public int price;
    public int weight;
    public WeaponType weaponType;
    public DamageType damageType;
    public string SubjectDisplay
    {
        get
        {
            if (subject == UpgradeSubject.Weapon)
            {
                return weaponType.ToString();
            }
            return subject.ToString();
        }
    }

    public void Buy(ClickEvent _)
    {
        if (subjectInstance == null)
        {
            InitSubjetInstance();
        }
        if (Player.Instance.CanAfford(price))
        {
            Player.Instance.ChangeBankValue(-price);
            subjectInstance.ApplyUpgrade(this);
        }
    }

    void InitSubjetInstance()
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
                List<RangedWeapon> weapons = GameObject.Find("Tower").GetComponent<Shooter>().Weapons;
                foreach (RangedWeapon weapon in weapons)
                {
                    if (weapon.name == weaponType.ToString() + "(Clone)")
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