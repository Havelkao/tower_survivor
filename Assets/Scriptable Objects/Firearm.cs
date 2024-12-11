using UnityEngine;

[CreateAssetMenu(fileName = "Firearm", menuName = "Scriptable Objects/Firearm")]
public class Firearm : ScriptableObject
{
    public int damage;
    public float attackSpeed;
    public float maxRange;
    public float minRange;
    public int multiStrike = 1;
    public Types.DamageType damageType;
    public float projectileSpeed;
    public bool isGroundWeapon;
    public GameObject projectilePrefab;
    public float mass;
    public float aoe;
}
