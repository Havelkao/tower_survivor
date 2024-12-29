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
    public Upgrade[] upgradeArray;
    [SerializeField] VisualTreeAsset myTreeAssetMember;


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

        foreach (var upgrade in upgradeArray)
        {
            TemplateContainer myUI = myTreeAssetMember.Instantiate();
            myUI.dataSource = upgrade;
            myUI.RegisterCallback<ClickEvent>(upgrade.Buy);
            upgrades.Add(myUI);
        }
    }


    public void SetWave(int waveIndex)
    {
        wave.text = $"Wave {waveIndex + 1}";
    }
}

