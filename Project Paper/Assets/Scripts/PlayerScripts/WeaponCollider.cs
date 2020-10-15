using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public float knockbackStrength = 100;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            //PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK1)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-20);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK2)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-35);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACK3)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-60);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
        }
    }
}
