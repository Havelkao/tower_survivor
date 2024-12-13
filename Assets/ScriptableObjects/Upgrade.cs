using UnityEngine;
using UnityEngine.UI;
using static Types;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradableProp property;
    public UpgradeSubject subject;
    public Image sprite;
    public float value;
    public int price;
    public int weight;
    public WeaponType weaponType;
    public DamageType damageType;
}