public class Player : Unit, IUpgradable, ITargetable
{
    public static Player Instance;
    private int income = 5;
    private int bank = 100;
    private readonly int incomePeriod = 1;
    protected override void Awake()
    {
        if (Instance == null)
        {
            base.Awake();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GameUI.Instance.bank.text = bank.ToString();
        GameUI.Instance.income.text = income.ToString();
        InvokeRepeating(nameof(GainIncome), 0, incomePeriod);
    }

    public void GainIncome()
    {
        bank += income;
        GameUI.Instance.bank.text = bank.ToString();
    }

    public void ChangeBankValue(int amount)
    {
        bank += amount;
        GameUI.Instance.bank.text = bank.ToString();

    }

    public void IncreaseIncome(int amount)
    {
        if (amount > 0)
        {
            income += amount;
            GameUI.Instance.income.text = income.ToString();
        }
    }

    public bool CanAfford(int amount)
    {
        return bank >= amount;
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.property)
        {
            case UpgradableProp.Income:
                IncreaseIncome((int)upgrade.value);
                break;
            case UpgradableProp.MaxHealth:
                health += upgrade.value;
                break;
            case UpgradableProp.HealthMulti:
                healthMulti += upgrade.value;
                break;
            case UpgradableProp.Armour:
                armour += (int)upgrade.value;
                break;
        }
    }
}




