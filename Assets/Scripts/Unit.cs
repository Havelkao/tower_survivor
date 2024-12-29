using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float healthMulti;
    public ArmourType armourType;
    public int armour;
    private Image healthBar;

    protected virtual void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentsInChildren<Image>()[1];
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(Weapon source)
    {
        float damage = source.damage * Utils.GetTypeMulti(source.damageType, armourType) * Utils.CalculateArmourEffect(armour);
        if (damage > 0)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        float reduction = (armour * 0.06f) / (1 + 0.06f * armour);
        float reduced = damage * Utils.GetTypeMulti(DamageType.Normal, armourType) * Utils.CalculateArmourEffect(armour);
        if (reduced > 0)
        {
            health -= reduced;
            healthBar.fillAmount = health / maxHealth;
        }
    }
}
