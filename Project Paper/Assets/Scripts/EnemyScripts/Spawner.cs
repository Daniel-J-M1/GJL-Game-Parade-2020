using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //Variables
    public GameObject Enemy;

    public BoxCollider SpawnArea;

    private Enemy Death;

    private int NoSpawned = 0;

    //Float Variables
    public float WaveLimit;
    public float GroupNo;
    public float Members;
    public float Rest;
    private float NoKilled = 0f;

    bool WaveStart;
    bool EnemyKilled;

    Vector3 Size;
    Vector3 Centre;

    private void Awake()
    {
        //Setting the Spawn Area Variables
        Transform CubeTransform = SpawnArea.GetComponent<Transform>();
        Centre = CubeTransform.position;

        Size.x = CubeTransform.localScale.x * SpawnArea.size.x;
        Size.y = 0;
        Size.z = CubeTransform.localScale.z * SpawnArea.size.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Starting the First Wave
        WaveStart = true;
    }

    // Setting up Random Enemy Spawn Locations Within Range
    private Vector3 GetRandomPosition()
    {
        Vector3 RandomPosition = new Vector3(Random.Range(-Size.x / 2, Size.x / 2), 1, Random.Range(-Size.z / 2, Size.z / 2));
        return Centre + RandomPosition;
    }

    //Starting and Stopping Coroutines
    void Update()
    {
        if (WaveStart == true)
        {
            StopCoroutine(NewWave());
            StartCoroutine(EnemySpawn());

            WaveStart = false;
        }
        else
        {

            if (NoSpawned == (Members * WaveLimit))
            {
                StopCoroutine(EnemySpawn());
                if (NoKilled == (Members * WaveLimit))
                {
                    print("false");
                    StartCoroutine(NewWave());
                    NoSpawned = 0;
                    NoKilled = 0;
                }
            }
        }
        }


    //Spawn Control
    IEnumerator EnemySpawn()
    {
        print("Start");
        while (NoSpawned < (Members * WaveLimit))
        {
            if (NoSpawned < (Members * GroupNo))
            {
                Instantiate(Enemy, GetRandomPosition(), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                NoSpawned = NoSpawned + 1;
                print("Spawn");
            }
            else
            {
                yield return new WaitForSeconds(5f);
                GroupNo = GroupNo + 1;
                print("Done");
            }
        }
    }

    //Setting Up New Wave
    IEnumerator NewWave()
    {
        print("NewWave");
        yield return new WaitForSeconds(Rest);
        WaveStart = true;
        WaveLimit += 1;
        GroupNo = 1;
        print("No. " + NoSpawned);

    }

    public void Died()
    {
            NoKilled = NoKilled + 1;
            print(NoKilled);
    }
}
