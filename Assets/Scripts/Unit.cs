using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public Transform ObjectTransform => transform;
    public float maxHealth;
    public float health;
    public float healthMulti;
    public ArmourType armourType;
    public int armour;
    private Image healthBar;
    private float thorns;
    private float healthOverTime;

    protected virtual void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentsInChildren<Image>()[1];
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyRecoveryOverTime()
    {
        if (healthOverTime != 0)
        {
            health += healthOverTime * Time.deltaTime;
            health = Mathf.Min(health, maxHealth);
        }
    }

    /// <summary>
    /// Unmitigated reflected damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(Weapon source)
    {
        float damage = source.Damage * Utils.GetTypeMulti(source.damageType, armourType) * Utils.CalculateArmourEffect(armour);
        if (damage > 0)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        }
    }

    public void TakeDamage(int damage, Unit unit)
    {
        float reduced = damage * Utils.GetTypeMulti(DamageType.Normal, armourType) * Utils.CalculateArmourEffect(armour);
        if (reduced > 0)
        {
            health -= reduced;
            healthBar.fillAmount = health / maxHealth;
        }

        if (thorns > 0)
        {
            unit.TakeDamage((int)thorns);
        }
    }

}