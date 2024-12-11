using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    private int income = 10;
    private readonly int incomePeriod = 5;
    public int bank = 100;

    void Start()
    {
        InvokeRepeating(nameof(GainIncome), 0, incomePeriod);
    }

    void GainIncome()
    {
        bank += income;
    }
    void GainMoney(int amount) { bank += amount; }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            health -= damage;
        }
    }

    public void IncreaseIncome(int amount)
    {
        if (amount > 0)
        {
            income += amount;
        }
    }
}




