using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float health;
    private Image healthBar;
    public float speed;
    public int damage;
    public int bounty;
    public Types.ArmourType armourType;
    public int range;
    public bool isFlying;
    private Vector3 target;

    private void Awake()
    {
        Vector3 targetPosition = GameObject.Find("Tower").transform.position;
        target = targetPosition - (targetPosition - transform.position).normalized * range;
        health = maxHealth;
        healthBar = transform.GetComponentsInChildren<Image>()[1];
    }


    public void Attack()
    {

    }

    private void Update()
    {
        if (health > 0)
        {
            if (!IsInRange())
            {
                Move();
            } else
            {
                Attack();
            }
        }
        else
        {
            Die();
        }
    }

    bool IsInRange()
    {
        return Vector3.Distance(transform.position, target) <= range;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        }
    }
    public void Move() 
    {
        transform.position = Vector3.MoveTowards(transform.position, target + new Vector3(0, transform.position.y, 0), speed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
