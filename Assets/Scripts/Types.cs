using UnityEngine;

public class Types
{
    public enum ArmourType
    {
        Unarmored = 0,
        Light = 1,
        Medium = 2,
        Heavy = 3
    }

    public enum DamageType
    {
        Normal = 0,
        Pierce = 1,
        Magic = 2,
        Chaos = 3
    };

    [System.Serializable]
    public struct WaveEnemy {
        public GameObject prefab;
        public float spawnRate;
    }
}
