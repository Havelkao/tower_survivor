using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Firearm firearm;
    private Transform target;
    private Rigidbody rb;

    public void Init(Firearm firearm, Transform target)
    {
        this.firearm = firearm;
        this.target = target;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = firearm.mass;
        if (target != null)
        {
            Fire();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        CollisionEffect(collision.gameObject);

        if (firearm.aoe > 0)
        {
            CheckAoE(transform.position);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firearm.aoe);
    }

    void CollisionEffect(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Enemy"))
        {
            Enemy enemy = collidedObject.GetComponent<Enemy>();
            enemy.TakeDamage(firearm.damage * Utils.GetDamageMulti(firearm.damageType, enemy.armourType));
        }
    }

    void Fire()
    {
        if (firearm.isGroundWeapon == true)
        {
            FireAtAngle();
        }
        else
        {
            FireDirectly();
        }
    }

    void FireDirectly()
    {
        Vector3 direction = target.transform.position - transform.position;
        rb.AddForce(firearm.projectileSpeed * direction, ForceMode.Impulse);
    }


    void FireAtAngle(float angle = 45f)
    {
        Vector3 direction = target.position - transform.position; // get Target Direction
        float heightDiff = direction.y;
        direction.y = 0; // retain only the horizontal difference
        float distance = direction.magnitude; // get horizontal direction
        float radians = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radians); // set dir to the elevation angle.
        distance += heightDiff / Mathf.Tan(radians); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * radians));
        if (velocity > 0) { 
            rb.linearVelocity = velocity * direction.normalized;        
        } else
        {
            FireDirectly();
        }
    }

    void CheckAoE(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, firearm.aoe);
        foreach (Collider collider in colliders)
        {
            CollisionEffect(collider.gameObject);
        }
    }

}