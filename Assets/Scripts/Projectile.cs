using UnityEngine;

public class Projectile : MonoBehaviour
{
    private RangedWeapon weapon;
    private Transform target;
    private Rigidbody rb;
    private int remainingChains;

    public void Init(RangedWeapon rangedWeapon, Transform target)
    {
        this.weapon = rangedWeapon;
        this.target = target;
        remainingChains = rangedWeapon.chainCount;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = weapon.mass;
        if (target != null)
        {
            FireAt(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollisionEffect(other.gameObject);

        if (weapon.aoe > 0)
        {
            CheckAoE(transform.position);
        }

        if (remainingChains > 0)
        {
            remainingChains--;
            //must be center around target, not projectile, for self-collision exclusion
            Transform[] inChainRange = TargetLocator.GetTransformsWithinRange(other.transform, weapon.chainDistance, (int)PhysicsLayer.Enemy);
            if (inChainRange.Length == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Transform[] nearestTargets = TargetLocator.GetFirst(inChainRange, 1);
                Debug.DrawLine(transform.position, nearestTargets[0].position, Color.red, 1f);
                FireAt(nearestTargets[0]);
            }

            return;
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        CollisionEffect(collision.gameObject);

        if (weapon.aoe > 0)
        {
            CheckAoE(transform.position);
        }

        Destroy(gameObject);
    }

    void CheckAoE(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, weapon.aoe, 1 << (int)PhysicsLayer.Enemy);
        foreach (Collider collider in colliders)
        {
            CollisionEffect(collider.gameObject);
        }
    }

    void CollisionEffect(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Enemy"))
        {
            Enemy enemy = collidedObject.GetComponent<Enemy>();
            enemy.TakeDamage(weapon);
        }

    }

    void FireAt(Transform target)
    {
        if (target == null) return;

        if (weapon.isGroundWeapon == true)
        {
            FireAtAngle(target);
        }
        else
        {
            FireDirectly(target);
        }
    }

    void FireDirectly(Transform target)
    {
        Vector3 direction = target.position - transform.localPosition;
        rb.linearVelocity = direction.normalized * weapon.projectileSpeed * 10;
    }

    void FireAtAngle(Transform target, float angle = 45f)
    {
        Vector3 direction = target.position - transform.position;
        float heightDiff = direction.y;
        direction.y = 0; // retain only the horizontal difference
        float distance = direction.magnitude; // get horizontal direction
        float radians = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radians); // set dir to the elevation angle.
        distance += heightDiff / Mathf.Tan(radians); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radians));
        if (velocity > 0)
        {
            rb.linearVelocity = velocity * direction.normalized;
        }
        else
        {
            FireDirectly(target);
        }
    }



}