using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public float speed = 10;
    Vector3 playerRotation;
    Rigidbody body;
    PlayerCombat playerCombat;
    bool dashing = false;
    int dashChargeCount;
    float dashChargeCooldown;

    bool invincible = false;
    float invTimer = 0;
    float maxInvTimer = 0.3f;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();
        body = GetComponent<Rigidbody>();
        dashChargeCount = 2;
        dashChargeCooldown = 2;

        //health bar settings
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float multiplier = speed * 100;


        if (playerCombat.attackState == PlayerCombat.AttackState.AS_SECONDARY)
            multiplier *= 0.5f;

        Vector3 input = Vector3.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        Vector3 direction = input.normalized;

        Vector3 movement = direction * multiplier;
        movement.y = body.velocity.y;

        //dash cooldown
        if (dashChargeCooldown > 0 && dashChargeCount < 2)
        {
            dashChargeCooldown -= Time.deltaTime;

        }
        else if(dashChargeCooldown <= 0 && dashChargeCount < 2)
        {
            dashChargeCount++;
            dashChargeCooldown = 2;
        }

        if (Input.GetKeyDown("e"))
        {
            AlterHealth(-10, false);
        }


        if (Input.GetKeyDown("left shift") && dashChargeCount > 0)
        {
            dashing = true;
            dashChargeCount--;
            StartCoroutine(Dash(movement));
        }

        if (!dashing)
            body.velocity = movement * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
        
        if (invincible)
        {
            invTimer -= Time.deltaTime;
            if (invTimer <= 0)
            {
                invincible = false;
            }
        }
    }

    IEnumerator Dash(Vector3 movement)
    {

        float timer = 0.05f;
        while (timer > 0)
        {
            body.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
            timer -= Time.deltaTime;
            yield return null;
        }

        body.velocity = new Vector3(0, body.velocity.y, 0);
        dashing = false;
        yield return null;
    }

    public void AlterHealth(int amount, bool inv)
    {

        if (!invincible)
        {
            health += amount;
            healthBar.value = health;
            invTimer = maxInvTimer;
            invincible = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AlterHealth(-5, true);
        }

        if (other.tag == "Boss Enemy")
        {
            GameObject boss = other.gameObject;
            if (boss.GetComponent<BossEnemy>().GetChargeState())
            {
                AlterHealth(-25, true);
                print("charge hit");
            }
            else
            {
                AlterHealth(-10, true);
                print("boss hit");
            }
        }
    }
}
