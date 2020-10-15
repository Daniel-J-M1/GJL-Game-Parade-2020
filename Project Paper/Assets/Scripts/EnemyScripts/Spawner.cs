using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;

    public float waveEnemyCount = 3;
    public float waveFrequency = 5;
    public float difficulty = 1;
    int currentlyAlive = 0;
    float waveTimer = 2;
    int currentWave = 1;
    int killCount = 0;

    GameObject[] spawnPoints;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        waveEnemyCount = 3;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randPos = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        return randPos;
    }

    //Starting and Stopping Coroutines
    void Update()
    {
        print("Currently alive");
        waveTimer -= Time.deltaTime;

        if(waveTimer <= 0)
        {
            switch (currentWave)
            {
                case 1:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 2:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 3:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 4:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 5:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 6:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                case 7:
                    StartCoroutine(SpawnWave((float)waveEnemyCount * difficulty));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator SpawnWave(float waveSize)
    {
        for (int i = 0; i < waveSize; i++)
        {
            Instantiate(Enemy, GetRandomPosition(), Quaternion.identity);
            currentlyAlive++;
        }
        while (waveTimer > 0)
        {
            yield return null;
        }

        waveEnemyCount += 1;
        difficulty *= 1.05f;
        waveTimer = waveFrequency * difficulty;
        currentWave++;
        yield return null;
    }

    public void Died()
    {
        killCount++;
        print(killCount);
    }
}
