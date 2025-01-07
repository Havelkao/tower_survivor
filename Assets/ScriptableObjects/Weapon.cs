using UnityEngine;


public class Weapon : ScriptableObject
{
    public WeaponType type;
    public float Damage { get { return (baseDamage + GetGlobals().baseDamage) * (damageMulti + GetGlobals().damageMulti); } }
    public int baseDamage;
    public float damageMulti;
    public float AttackSpeed { get { return baseAttackSpeed * (attackSpeedMulti + GetGlobals().attackSpeedMulti); } }
    public float baseAttackSpeed;
    public float attackSpeedMulti;
    public DamageType damageType;
    public float range;
    public float aoe;
    public bool isGroundWeapon;
    private DamageTypeMulti GetGlobals() => GlobalWeaponModifiers.damageTypesMultipliers[damageType];
}