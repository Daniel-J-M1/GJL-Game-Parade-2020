using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //Variables
    GameObject player;
    int currentPoint;
    private Rigidbody body;

    bool moving = false;
    int maxMoveCount = 2;
    int currMoveCount = 0;
    bool attacked = true;

    public float chargeSpeed = 50f;
    public float maxSpeed = 10f;

    float maxHealth;
    public float health = 100f;

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
        attacked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            player.gameObject.GetComponent<Spawner>().Died();
        }
        if (health > maxHealth / 2)
        {
            float r = 1 - (health / maxHealth);
            indicator.GetComponent<Renderer>().material.color = new Color(r, 1f, 0f, 1f);
        }
        else
        {
            float g = 1 - (health / maxHealth);
            indicator.GetComponent<Renderer>().material.color = new Color(1f, g, 0f, 1f);
        }


        if (!moving && attacked)
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
        GameObject[] randomPoints = GameObject.FindGameObjectsWithTag("Walk Point");
        int randPointNew = -1;
        randPointNew = Random.Range(0, randomPoints.Length);

        while (randPointNew == currentPoint)
        {
            randPointNew = Random.Range(0, randomPoints.Length);
        }
        currentPoint = randPointNew;

        return randomPoints[currentPoint];
    }

    IEnumerator ChargeAttack()
    {
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
        attacked = true;
        yield return null;
    }

    IEnumerator Wander(GameObject spawnPos)
    {
        moving = true;
        attacked = false;
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
        if(currMoveCount <= maxMoveCount)
        {
            StartCoroutine(Wander(GetRandomPoint()));
        }
        else
        {
            print("Moved 5 times");
            currMoveCount = 0;
            moving = false;
            StartCoroutine(ChargeAttack());
        }
        yield return null;
    }

    public void AlterHealth(float altHP)
    {
        if (!isInvincible)
        {
            health += altHP;
            isInvincible = true;
            invincibleTimer = maxIvincibilityTime;
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
