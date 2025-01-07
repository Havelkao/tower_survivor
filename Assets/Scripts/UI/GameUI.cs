using UnityEngine;
using UnityEngine.UIElements;


public class GameUI : UI
{
    public static GameUI Instance;
    public Label waveCountdown;
    public Label wave;
    public Label bank;
    public Label income;
    public VisualElement upgrades;
    public VisualElement newWeapon;

    public Upgrade[] upgradeArray;
    [SerializeField] VisualTreeAsset upgradeTemplate;
    public RangedWeapon[] weaponArray;
    [SerializeField] VisualTreeAsset newWeaponTemplate;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Hide();
        waveCountdown = document.rootVisualElement.Q<Label>("WaveTimer");
        wave = document.rootVisualElement.Q<Label>("WaveDisplay");
        bank = document.rootVisualElement.Q<Label>("Bank");
        income = document.rootVisualElement.Q<Label>("Income");
        upgrades = document.rootVisualElement.Q<VisualElement>("Upgrades");
        newWeapon = document.rootVisualElement.Q<VisualElement>("WeaponSelection");
        Shooter shooter = GameObject.Find("Tower").GetComponent<Shooter>();

        foreach (var upgrade in upgradeArray)
        {
            TemplateContainer template = upgradeTemplate.Instantiate();
            // instance makes object call awake 
            Upgrade upgradeInstance = Instantiate(upgrade);
            template.dataSource = upgradeInstance;
            template.RegisterCallback<ClickEvent>(upgradeInstance.Buy);
            upgrades.Add(template);
        }

        foreach (var weapon in weaponArray)
        {
            TemplateContainer template = newWeaponTemplate.Instantiate();
            template.dataSource = weapon;
            template.RegisterCallback<ClickEvent>((evt) => shooter.AddWeapon(evt, weapon));
            newWeapon.Add(template);
        }
    }



    public void SetWave(int waveIndex)
    {
        wave.text = $"Wave {waveIndex + 1}";
    }
}

