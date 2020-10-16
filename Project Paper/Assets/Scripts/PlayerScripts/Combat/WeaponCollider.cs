using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public float knockbackStrength = 300;
    public float baseDamage = 20;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Spawner Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage);

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage * 1.5f);

            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
                other.gameObject.GetComponent<SpawnerEnemy>().AlterHealth(-baseDamage * 3);
        }

        if (other.gameObject.tag == "Boss Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();
            
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage);
            
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage * 1.5f);
            
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
                other.gameObject.GetComponent<BossEnemy>().AlterHealth(-baseDamage * 3);
        }

        if (other.gameObject.tag == "Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            //PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage * 1.5f, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-baseDamage * 3, true);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
        }
    }
}
