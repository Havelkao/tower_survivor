using UnityEngine;

public class Enemy : Unit
{
    public int baseDamage;
    public float damageMultiplier;
    public float movementSpeed;
    public float movementSpeedMultiplier;
    public float baseAttackSpeed;
    public float attackSpeedMultiplier;
    public int bounty;
    public int range;
    public bool isFlying;
    private Vector3 target;
    private bool isAttacking = false;
    private Weapon weapon;
    private bool IsInRange { get { return Vector3.Distance(transform.position, target) <= range; } }

    protected override void Awake()
    {
        base.Awake();
        Vector3 targetPosition = GameObject.Find("Tower").transform.position;
        target = targetPosition - (targetPosition - transform.position).normalized * range;
    }


    public virtual void Attack()
    {
        Player.Instance.TakeDamage(baseDamage);
        if (IsInRange)
        {
            Invoke(nameof(Attack), 1 / baseAttackSpeed);
        }
        else
        {
            isAttacking = false;
        }
    }

    private void Update()
    {
        if (health > 0)
        {
            if (!IsInRange)
            {
                Move();
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                Invoke(nameof(Attack), 0);
            }
        }
        else
        {
            Die();
        }
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target + new Vector3(0, transform.position.y, 0), movementSpeed * Time.deltaTime);
    }

    protected override void Die()
    {
        base.Die();
        Player.Instance.ChangeBankValue(bounty);
    }

}
