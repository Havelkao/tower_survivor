using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooter : MonoBehaviour
{
    //to do getter setter
    public List<RangedWeapon> _weapons;
    public List<RangedWeapon> weapons = new();
    private Transform projectileContainer;

    void Start()
    {
        projectileContainer = transform.Find("Projectiles");
        if (projectileContainer == null)
        {
            projectileContainer = transform;
        }

        foreach (RangedWeapon _weapon in _weapons)
        {
            RangedWeapon instance = Instantiate(_weapon);
            weapons.Add(instance);
        }

        foreach (RangedWeapon weapon in weapons)
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

            Transform[] targetsInRange = GetObjectsWithinRange(SpawnManager.Instance.transform, weapon.range, (int)Types.PhysicsLayer.Enemy);
            Transform[] targets = new Transform[0];
            switch (weapon.targetMode)
            {
                case Types.TargetMode.Newest:
                    targets = GetNewestEntities(targetsInRange, weapon.multiStrike);
                    break;
                case Types.TargetMode.Oldest:
                    targets = GetClosestEntity(targetsInRange, weapon.multiStrike, SpawnManager.Instance.transform);
                    break;
                case Types.TargetMode.Closest:
                    targets = GetOldestEntites(targetsInRange, weapon.multiStrike);
                    break;
                case Types.TargetMode.Random:
                    targets = GetRandomEntites(targetsInRange, weapon.multiStrike);
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

    Transform[] GetObjectsWithinRange(Transform parent, float distance, int layer)
    {
        
        Collider[] colliders = Physics.OverlapSphere(parent.position, distance, 1 << layer);
        Transform[] result = new Transform[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            
            result[i] = colliders[i].transform;
        }

        return result;
    }

    Transform[] GetRandomEntites(Transform[] entities, int count)
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
            while (selectedIndices.Contains(randomIndex));  // Ensure uniqueness

            selectedIndices.Add(randomIndex);
            randomEntity[i] = entities[randomIndex];
        }

        return randomEntity;
    }

    Transform[] GetNewestEntities(Transform[] entities, int count)
    {
        int length = entities.Length;
        if (length == 0)
        {
            return new Transform[0];
        }

        if (entities.Length <= count)
        {
            return entities;
        }

        int maxCount = Mathf.Min(count, entities.Length);
        Transform[] result = new Transform[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            result[i] = entities[i];
        }

        return result;
    }

    Transform[] GetOldestEntites(Transform[] entities, int count)
    {
        int length = entities.Length;
        if (length == 0)
        {
            return new Transform[0];
        }

        if (entities.Length <= count)
        {
            return entities;
        }

        int maxCount = Mathf.Min(count, entities.Length);
        Transform[] result = new Transform[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            result[i] = entities[i];
        }

        return result;
    }

    public Transform[] GetClosestEntity(Transform[] entities, int count, Transform target)
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