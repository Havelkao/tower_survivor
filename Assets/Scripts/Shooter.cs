using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public List<RangedWeapon> weaponPrototypes;
    private List<RangedWeapon> _weapons = new();
    public List<RangedWeapon> Weapons
    {
        get
        {
            if (_weapons.Count == 0)
            {
                foreach (RangedWeapon weapon in weaponPrototypes)
                {
                    RangedWeapon instance = Instantiate(weapon);
                    _weapons.Add(instance);
                }
                return _weapons;
            }
            else { return _weapons; }
        }
        set
        {
            weaponPrototypes.AddRange(value);
            foreach (RangedWeapon newValue in value)
            {
                RangedWeapon instance = Instantiate(newValue);
                _weapons.Add(instance);
            }
        }
    }
    private Transform projectileContainer;


    void Awake()
    {
        projectileContainer = transform.Find("Projectiles");
        if (projectileContainer == null)
        {
            projectileContainer = transform;
        }


        foreach (RangedWeapon weapon in Weapons)
        {
            StartCoroutine(Fire(weapon));
        }
    }

    IEnumerator Fire(RangedWeapon weapon)
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / weapon.attackSpeed);

            if (SpawnManager.Instance.transform.childCount == 0) continue;

            Transform[] targetsInRange = TargetLocator.GetTransformsWithinRange(SpawnManager.Instance.transform, weapon.range, (int)PhysicsLayer.Enemy);
            Transform[] targets = new Transform[0];
            switch (weapon.targetMode)
            {
                case TargetMode.Oldest:
                    targets = TargetLocator.GetFirst(targetsInRange, weapon.projectileCount);
                    break;
                case TargetMode.Closest:
                    targets = TargetLocator.GetClosest(targetsInRange, weapon.projectileCount, SpawnManager.Instance.transform);
                    break;
                case TargetMode.Newest:
                    targets = TargetLocator.GetLast(targetsInRange, weapon.projectileCount);
                    break;
                case TargetMode.Random:
                    targets = TargetLocator.GetRandom(targetsInRange, weapon.projectileCount);
                    break;
            }

            if (targets.Length == 0) continue;

            foreach (var target in targets)
            {
                GameObject projectile = Instantiate(weapon.projectilePrefab, transform.position + Vector3.up * 3, transform.rotation, projectileContainer);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.Init(weapon, target);
                Destroy(projectile, 5);
            }
        }
    }
}

public static class TargetLocator
{
    public static Transform[] GetTransformsWithinRange(Transform center, float distance, int layer)
    {
        List<Transform> result = new();

        Collider[] colliders = Physics.OverlapSphere(center.position, distance, 1 << layer);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == center.gameObject)
            {
                continue;
            }
            result.Add(collider.transform);
        }

        return result.ToArray<Transform>();
    }

    public static Transform[] GetRandom(Transform[] entities, int count)
    {
        int length = entities.Length;
        if (length == 0)
        {
            return new Transform[0];
        }

        if (length <= count)
        {
            return entities;
        }

        HashSet<int> selectedIndices = new();
        Transform[] randomEntity = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, length);
            }
            while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);
            randomEntity[i] = entities[randomIndex];
        }

        return randomEntity;
    }

    public static Transform[] GetFirst(Transform[] entities, int count)
    {
        if (entities.Length == 0)
        {
            return new Transform[0];
        }

        if (entities.Length <= count)
        {
            return entities;
        }

        return entities.Take(count).ToArray();
    }

    public static Transform[] GetLast(Transform[] entities, int count)
    {
        if (entities.Length == 0)
        {
            return new Transform[0];
        }

        if (entities.Length <= count)
        {
            return entities;
        }

        return entities.TakeLast(count).ToArray();
    }




    public static Transform[] GetClosest(Transform[] entities, int count, Transform target)
    {
        int length = entities.Length;
        if (length == 0)
        {
            return new Transform[0];
        }

        if (length == 1)
        {
            return new Transform[] { entities[0] };
        }

        int maxCount = Mathf.Min(count, length);
        SortedList<float, Transform> sortedList = new();

        foreach (Transform entity in entities)
        {
            float squaredMagnitude = (entity.position - target.position).sqrMagnitude;
            sortedList[squaredMagnitude] = entity;

            if (sortedList.Count > maxCount)
            {
                sortedList.RemoveAt(sortedList.Count - 1);
            }
        }

        Transform[] closest = new Transform[maxCount];
        sortedList.Values.CopyTo(closest, 0);
        return closest;
    }
}