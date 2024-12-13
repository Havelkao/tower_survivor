using UnityEngine;
using static Types;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Scriptable Objects/RangedWeapon")]
public class RangedWeapon : Weapon, IUpgradable
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float mass;
    public int projectileCount;
    public int chainCount;
    public Types.TargetMode targetMode;

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.baseDamage:
                baseDamage += (int)upgrade.value;
                break;
            case UpgradableProp.damageMulti:
                damageMulti += (int)upgrade.value;
                break;
            case UpgradableProp.attackSpeedMulti:
                attackSpeedMulti += upgrade.value;
                break;
            case UpgradableProp.projectileCount:
                projectileCount += (int)upgrade.value;
                break;
            case UpgradableProp.aoe:
                aoe += upgrade.value;
                break;
            case UpgradableProp.chainCount:
                damage += (int)upgrade.value;
                break;
        }
    }
}
