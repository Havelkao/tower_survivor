using JetBrains.Annotations;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using static Types;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField]
    public Wave[] waves;
    private float spawnDistance = 15f;
    private Wave currentWave;
    private int currentWaveIndex = 0;
    private int waveDuration = 15;
    private int breakDuration = 5;
    private TextMeshProUGUI waveDisplay;
    private Timer timer;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        waveDisplay = GameObject.Find("WaveDisplay").GetComponent<TextMeshProUGUI>();
        waveDisplay.text = $"Wave {currentWaveIndex + 1}";
        currentWave = waves[0];
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
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
                waveDisplay.text = $"Wave {currentWaveIndex + 1}";
        }
    }

    IEnumerator Spawn(WaveEnemy enemy)
    {
        //must manually dispose of the coroutine
        while (true)
        {
            Vector3 spawnPosition = GetRandomPosition(enemy.prefab.transform.position.y);
            Instantiate(enemy.prefab, spawnPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(1 / enemy.spawnRate);
        }
        
    }

    void Spawn(GameObject prefab)
    {
        Vector3 spawnPosition = GetRandomPosition(prefab.transform.position.y);
        Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
    }

    Vector3 GetRandomPosition(float y)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(randomAngle) * spawnDistance;
        float z = Mathf.Sin(randomAngle) * spawnDistance;
        return new Vector3(x, y, z);
    }
}

