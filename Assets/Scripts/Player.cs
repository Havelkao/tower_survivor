using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Types;

public class Player : Unit, Types.IUpgradable
{
    public static Player Instance;
    public int income = 10;
    public int bank = 100; 
    private readonly int incomePeriod = 5;
    private TextMeshProUGUI bankDisplay;
    private TextMeshProUGUI incomeDisplay;

    protected override void Awake()
    {
        if (Instance == null)
        {
            base.Awake();
            Instance = this;
        }
    }
    void Start()
    {
        bankDisplay = GameObject.Find("BankValue").GetComponent<TextMeshProUGUI>();
        bankDisplay.text = bank.ToString();
        incomeDisplay = GameObject.Find("IncomeValue").GetComponent<TextMeshProUGUI>();
        incomeDisplay.text = income.ToString();

        InvokeRepeating(nameof(GainIncome), 0, incomePeriod);
    }

    public void GainIncome()
    {
        bank += income;
        bankDisplay.text = bank.ToString();
    }

    public void ChangeBankValue(int amount) { 
        bank += amount;
        bankDisplay.text = bank.ToString();
    }

    public void IncreaseIncome(int amount)
    {
        if (amount > 0)
        {
            income += amount;
            incomeDisplay.text = income.ToString();
        }
    }
    
    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.baseDamage:
                IncreaseIncome((int)upgrade.value);
                break;
            case UpgradableProp.maxHealth:
                health += upgrade.value;
                break;
            case UpgradableProp.healthMulti:
                healthMulti += upgrade.value;
                break;
            case UpgradableProp.armour:
                armour += (int)upgrade.value;
                break;
        }
    }
}




