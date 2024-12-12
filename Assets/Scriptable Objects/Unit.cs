using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private Image healthBar;
    public Types.ArmourType armourType;
    public int armour;

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
        float reduction = (armour * 0.06f) / (1 + 0.06f * armour);
        float damage = source.damage * Utils.GetDamageMulti(source.damageType, armourType) / (1 + reduction);
        if (damage > 0)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        float reduction = (armour * 0.06f) / (1 + 0.06f * armour);
        float reduced = damage * Utils.GetDamageMulti(Types.DamageType.Normal, armourType) / (1 + reduction);
        if (reduced > 0)
        {
            health -= reduced;
            healthBar.fillAmount = health / maxHealth;
        }
    }
}
