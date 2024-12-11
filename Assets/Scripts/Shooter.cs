using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Firearm _firearm;
    private Firearm firearm;

    void Start()
    {
        firearm = Instantiate(_firearm);
        Invoke(nameof(Fire), 1);
    }

    private void OnEnable()
    {
        Invoke(nameof(Fire), 0);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(Fire));
    }

    void Fire()
    {
        Invoke(nameof(Fire), 1 / firearm.attackSpeed);
        if (SpawnManager.Instance.transform.childCount == 0) return;

        Transform[] enemies = GetOldestTargets(SpawnManager.Instance.transform, firearm.multiStrike);
        //Transform[] enemies = GetRandomTarget(EnemySpawner.Instance.transform);
        if (enemies.Length == 0) return;

        foreach (var enemy in enemies)
        {
            GameObject projectile = Instantiate(firearm.projectilePrefab, transform.position + Vector3.up * 3, transform.rotation, transform.Find("Projectiles"));
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Init(firearm, enemy);
            Destroy(projectile, 5);
        }
    }

    public void ChangeMultistrike(int i)
    {
        firearm.multiStrike += i;
    }

    Transform[] GetRandomTarget(Transform parent, int n = 1)
    {
        //int maxCount = n > parent.childCount ? parent.childCount : n;
        Transform[] targets = new Transform[1];
        targets[0] = parent.GetChild(Random.Range(0, parent.childCount - 1));

        return targets;
    }

    Transform[] GetOldestTargets(Transform parent, int n)
    {
        int maxCount = n > parent.childCount ? parent.childCount : n;
        Transform[] targets = new Transform[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            if (parent.GetChild(i))
            {
                targets[i] = parent.GetChild(i);

            }
        }
        return targets;
    }

    Transform GetClosestTarget(Transform parent)
    {
        Transform closestChild = null;
        float closestDistanceSqr = Mathf.Infinity;

        Vector3 parentPosition = parent.position;

        foreach (Transform child in parent)
        {
            if (!child.CompareTag("Enemy"))
            {
                continue;
            }
            float distanceSqr = (child.position - parentPosition).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestChild = child;
            }
        }

        return closestChild;
    }

    

}