﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //Variables
    public GameObject enemy;
    GameObject player;
    int currentPoint;
    private Rigidbody body;

    bool moving = false;
    int maxMoveCount = 1;
    int currMoveCount = 0;
    bool attacking = false;


    public float spawnerDelay = 10;
    float currentDelay;
    bool spawning = false;
    float currEnemiesSpawned = 0;
    float maxEnemiesSpawned = 2;

    public float chargeSpeed = 50f;
    public float maxSpeed = 10f;

    float maxHealth;
    public float health = 1000f;

    bool isInvincible = false;
    public float maxIvincibilityTime = 0.2f;
    float invincibleTimer;

    Transform indicator;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning Variable Values
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
        maxHealth = health;
        indicator = transform.Find("Indicator");
        moving = false;
        attacking = false;
        currentDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            player.gameObject.GetComponent<Spawner>().Died();
        }
        if (health > maxHealth / 2)
        {
            float r = 1 - (float)(health / maxHealth);
            indicator.GetComponent<Renderer>().material.color = new Color(r, 1f, 0f, 1f);
        }
        else
        {
            float g = (health / maxHealth * 2);
            indicator.GetComponent<Renderer>().material.color = new Color(1f, g, 0f, 1f);
        }


        if (!moving && !attacking && !spawning)
        {
            GameObject randomPoint = GetRandomPoint();   
            StartCoroutine(Wander(randomPoint));
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                ResetInvincible();
            }
        }
    }

    GameObject GetRandomPoint()
    {
        GameObject[] randomPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        int randPointNew = -1;
        randPointNew = Random.Range(0, randomPoints.Length);

        while (randPointNew == currentPoint)
        {
            randPointNew = Random.Range(0, randomPoints.Length);
        }
        currentPoint = randPointNew;

        return randomPoints[currentPoint];
    }

    Vector3 GetPointInRadious()
    {
        Vector3 offset = Random.insideUnitCircle * 10;
        offset.y = 0f;
        return transform.position + offset;
    }

    IEnumerator ChargeAttack()
    {
        attacking = true;
        var playerPos = player.transform.position;
        Vector3 Direction = player.transform.position - transform.position;
        Direction.Normalize();
        Direction.y = 0;

        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
            yield return null;
        }
        float chargeTimer = 0.5f;
        while (chargeTimer > 0)
        {
            chargeTimer -= Time.deltaTime;
            body.MovePosition(transform.position + Direction * chargeSpeed * Time.deltaTime);
            yield return null;
        }
        attacking = false;
        yield return null;
    }

    IEnumerator SpawnEnemies()
    {
        spawning = true;
        float timer = 0.5f;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (currEnemiesSpawned < maxEnemiesSpawned)
        {
            Instantiate(enemy, GetPointInRadious(), Quaternion.identity);
            StartCoroutine(SpawnEnemies());
            currEnemiesSpawned++;
        }
        else
        {
            attacking = false;
            moving = false;
            spawning = false;
            currEnemiesSpawned = 0;
            maxEnemiesSpawned = 2 + Random.Range(-1, 1);
        }
        yield return null;
    }

    IEnumerator Wander(GameObject spawnPos)
    {
        moving = true;
        float timer = 3;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Vector3 Direction = spawnPos.transform.position - transform.position;
            transform.LookAt(new Vector3(spawnPos.transform.position.x, transform.position.y, spawnPos.transform.position.z));
            Direction.Normalize();
            Direction.y = 0;            

            body.MovePosition(transform.position + Direction * maxSpeed * Time.deltaTime);
            yield return null;
        }
        currMoveCount++;
        if(currentDelay < 0)
        {
            currentDelay = spawnerDelay;
            StartCoroutine(SpawnEnemies());
        }
        else if (currMoveCount <= maxMoveCount)
        {
            StartCoroutine(Wander(GetRandomPoint()));
        }
        else
        {
            currMoveCount = 0;
            moving = false;
            StartCoroutine(ChargeAttack());
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            AlterHealth(-5, false);
        }
    }

    public void AlterHealth(float altHP, bool inv)
    {
        if (!isInvincible)
        {
            health += altHP;
            if (inv)
            {
                isInvincible = true;
                invincibleTimer = maxIvincibilityTime;
            }
        }
    }

    void ResetInvincible()
    {
        isInvincible = false;
    }

    public void KnockBack(float intensity)
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        dir.Normalize();
        body.velocity = dir * intensity;
    }
}
