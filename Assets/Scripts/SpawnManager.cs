using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField]
    public Wave[] waves;
    private float spawnDistance = 15f;
    private Wave currentWave;
    private int currentWaveIndex = 0;
    private float waveDuration = 15f;
    private int breakDuration = 5;
    private Timer timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        timer = GameObject.Find("Game").GetComponent<Timer>();
        timer.timeRemaining = waveDuration;
        currentWave = waves[0];
    }

    public void Run()
    {
        GameUI.Instance.SetWave(currentWaveIndex);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        timer.isActive = true;

        while (currentWaveIndex < waves.Length)
        {
            Coroutine[] coroutines = new Coroutine[currentWave.enemies.Length];

            timer.timeRemaining = waveDuration;

            for (int i = 0; i < coroutines.Length; i++)
            {
                coroutines[i] = StartCoroutine(Spawn(currentWave.enemies[i]));
            }
            yield return new WaitForSeconds(waveDuration);

            foreach (Coroutine coroutine in coroutines)
            {
                StopCoroutine(coroutine);
            }

            Spawn(currentWave.boss);
            timer.timeRemaining = breakDuration;
            yield return new WaitForSeconds(breakDuration);

            if (currentWaveIndex == waves.Length - 1)
            {
                break;
            }
            currentWaveIndex++;
            currentWave = this.waves[currentWaveIndex];
            GameUI.Instance.SetWave(currentWaveIndex);
        }
    }

    IEnumerator Spawn(WaveEnemy enemy)
    {
        //must manually dispose of the coroutine
        while (true)
        {
            Spawn(enemy.prefab);
            yield return new WaitForSeconds(1 / enemy.spawnRate);
        }

    }

    void Spawn(GameObject prefab)
    {
        Vector3 spawnPosition = GetRandomPosition(prefab.transform.position.y);
        Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        //instance.GetComponent<Enemy>().SetTarget(Player.Instance);
    }

    Vector3 GetRandomPosition(float y)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(randomAngle) * spawnDistance;
        float z = Mathf.Sin(randomAngle) * spawnDistance;
        return new Vector3(x, y, z);
    }
}

