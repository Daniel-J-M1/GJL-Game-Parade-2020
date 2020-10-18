using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : MonoBehaviour
{
    GameObject player;
    private Rigidbody body;

    public float maxSpeed = 4f;
    public float chargeSpeed = 50f;
    public bool killed = false;

    float maxHealth = 500f;
    public float health;

    bool charging = false;
    bool shooting = false;
    bool moving = false;

    bool isInvincible = false;
    public float maxIvincibilityTime = 0.2f;
    float invincibleTimer;
    bool wallHit = false;

    Transform indicator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponent<Rigidbody>();
        maxSpeed = maxSpeed + Random.Range(-2f, 2f);
        health = maxHealth;
        indicator = transform.Find("Indicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //PlayMusic.BaseMusic();
            Destroy(this.gameObject);
            player.gameObject.GetComponent<Spawner>().Died();
            player.GetComponent<PlayerController>().AlterCash(10);
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



    }
}
