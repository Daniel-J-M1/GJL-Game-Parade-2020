using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnerEnemy;
    public GameObject bossEnemy;
    public bool paused;

    public float waveEnemyCount = 3;
    public float waveFrequency = 5;
    public float difficulty = 1.1f;
    int currentlyAlive = 0;
    float waveTimer = 2;
    int currentWave = 1;
    int killCount = 0;

    GameObject[] spawnPoints;
    GameObject[] bossSpawnPoints;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        bossSpawnPoints = GameObject.FindGameObjectsWithTag("Boss Walk Point");
        waveEnemyCount = 3;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randPos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        return randPos;
    }
    private Vector3 GetRandomBossPosition()
    {
        Vector3 randPos = bossSpawnPoints[Random.Range(0, bossSpawnPoints.Length)].transform.position;
        randPos.y = 1.5f;
        return randPos;
    }

    //Starting and Stopping Coroutines
    void Update()
    {
        if (!paused)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                switch (currentWave)
                {
                    case 1:
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 0));
                        break;
                    case 2:
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 1));
                        break;
                    case 3:
                        waveFrequency += 3;
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 0));
                        break;
                    case 4:
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 2));
                        break;
                    case 5:
                        waveFrequency += 3;
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 0));
                        break;
                    case 6:
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 2));
                        break;
                    case 7:
                        StartCoroutine(SpawnWave((float)waveEnemyCount, 2));
                        break;
                    case 8:
                        Instantiate(bossEnemy, GetRandomBossPosition(), Quaternion.identity);
                        currentWave++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    IEnumerator SpawnWave(float waveSize, float spawnerCount)
    {
        for (int i = 0; i < waveSize; i++)
        {
            Instantiate(enemy, GetRandomPosition(), Quaternion.identity);
            currentlyAlive++;
        }
        for (int i = 0; i < spawnerCount; i++)
        {
            Instantiate(spawnerEnemy, GetRandomPosition(), Quaternion.identity);
            currentlyAlive++;
        }

        while (waveTimer > 0)
        {
            yield return null;
        }

        waveEnemyCount *= difficulty;
        waveTimer = waveFrequency * 1.2f;
        currentWave++;
        yield return null;
    }

    public void Died()
    {
        killCount++;
        print(killCount);
    }
}
