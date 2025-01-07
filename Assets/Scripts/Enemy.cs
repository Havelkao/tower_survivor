using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IDebuffable
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
    private Vector3 attackPosition;
    public bool isAttacking = false;
    private bool IsInRange { get { return Vector3.Distance(transform.position, attackPosition) <= range; } }
    public bool isStunned = false;
    public bool isImpaired = false;
    private ITargetable target;
    private Dictionary<DebuffableProp, Debuff> activeDebuffs = new();


    public void SetTarget(ITargetable target)
    {
        this.target = target;
        attackPosition = target.ObjectTransform.position - (target.ObjectTransform.position - transform.position).normalized * range;
    }

    public virtual void Attack()
    {
        target.TakeDamage(baseDamage, this);
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
        if (health > 0 && !isStunned)
        {
            if (!IsInRange && !isImpaired)
            {
                Move();
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                Invoke(nameof(Attack), 0);
            }
        }

        if (health < 0)
        {
            Die();
        }
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, attackPosition + new Vector3(0, transform.position.y, 0), movementSpeed * Time.deltaTime);
    }

    protected override void Die()
    {
        base.Die();
        Player.Instance.ChangeBankValue(bounty);
    }

    public void ApplyDebuff(Debuff debuff)
    {
        if (activeDebuffs.ContainsKey(debuff.prop))
        {
            return;
        }

        switch (debuff.prop)
        {
            case DebuffableProp.Armour:
                debuff.SetCallbacks(() => (float)armour, (value) => armour += (int)value);
                break;
            case DebuffableProp.MovementSpeedMulti:
                break;

        }
        activeDebuffs[debuff.prop] = debuff;
        StartCoroutine(debuff.ApplyWithRevert(this));
    }

    public void RemoveDebuff(Debuff debuff)
    {
        if (activeDebuffs.ContainsKey(debuff.prop))
        {
            activeDebuffs.Remove(debuff.prop);
        }
    }

}
