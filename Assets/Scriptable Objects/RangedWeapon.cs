using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Scriptable Objects/RangedWeapon")]
public class RangedWeapon : Weapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float mass;
    public int multiStrike;
    public Types.TargetMode targetMode;
}
