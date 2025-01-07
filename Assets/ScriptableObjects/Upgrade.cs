using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradableProp property;
    public UpgradeSubject subject;
    private IUpgradable subjectInstance;
    //public Image sprite;
    public float value;
    public int price;
    public int weight;
    public WeaponType weaponType;
    public DamageType damageType;
    public string subjectDisplay;

    private void Awake()
    {
        // getters not working with ui toolkit data binding
        subjectDisplay = subject.ToString();
        if (subject == UpgradeSubject.Weapon)
        {
            subjectDisplay = weaponType.ToString();
        }
    }

    public void Buy(ClickEvent _)
    {
        InitSubjetInstance();
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
                subjectInstance = GameObject.Find("Tower").GetComponent<Shooter>().weapons[weaponType];
                break;
            default:
                throw new System.Exception("No upgrade subject found!");
        }
    }
}