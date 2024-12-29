using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Scriptable Objects/RangedWeapon")]
public class RangedWeapon : Weapon, IUpgradable
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float mass;
    public int projectileCount;
    public int chainCount;
    public int chainDistance;
    public TargetMode targetMode;

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.BaseDamage:
                baseDamage += (int)upgrade.value;
                break;
            case UpgradableProp.DamageMulti:
                damageMulti += (int)upgrade.value;
                break;
            case UpgradableProp.AttackSpeedMulti:
                attackSpeedMulti += upgrade.value;
                break;
            case UpgradableProp.ProjectileCount:
                projectileCount += (int)upgrade.value;
                break;
            case UpgradableProp.Aoe:
                aoe += upgrade.value;
                break;
            case UpgradableProp.ChainCount:
                chainCount += (int)upgrade.value;
                break;
        }
    }
}
