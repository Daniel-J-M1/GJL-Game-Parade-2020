﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public float knockbackStrength = 300;
    public float baseDamage = 20;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spawner Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
            {
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage, false);
                playerCombat.SetAmmo(3);
            }

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
            {
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage * 1.5f, false);
                playerCombat.SetAmmo(3);
            }

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
            {
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage * 3, false);
                playerCombat.SetAmmo(4);
            }
        }

        if (other.gameObject.tag == "Boss Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
            {
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage, false);
                playerCombat.SetAmmo(4);
            }

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
            {
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage * 1.5f, false);
                playerCombat.SetAmmo(5);
            }

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
            {
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage * 3, false);
                playerCombat.SetAmmo(7);
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            //PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
                playerCombat.SetAmmo(2);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage * 1.5f, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
                playerCombat.SetAmmo(2);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage * 3, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
                playerCombat.SetAmmo(3);
            }
        }
    }
}
