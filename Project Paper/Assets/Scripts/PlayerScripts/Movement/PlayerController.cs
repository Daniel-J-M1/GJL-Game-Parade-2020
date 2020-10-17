﻿using System.Collections;
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
    public Image dashImg1;
    public Image dashImg2;
    public Text cashText;

    private float cash;

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
        cash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Color colorActive = new Color(1f, 1f, 1f, 1f);
        Color colorNotActive = new Color(1f, 1f, 1f, 0f);
        if (dashChargeCount == 2)
        {
            dashImg1.color = colorActive;
            dashImg2.color = colorActive;
        }
        else if (dashChargeCount == 1)
        {
            dashImg1.color = colorActive;
            dashImg2.color = colorNotActive;
        }
        else
        {
            dashImg1.color = colorNotActive;
            dashImg2.color = colorNotActive;
        }

        cashText.text = cash.ToString();
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

    public float GetCash()
    {
        return cash;
    }

    public void AlterCash(int amount)
    {
        cash += amount;
    }
}
