using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public float knockbackStrength = 100;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

            //PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            if (playerCombat.attackState == PlayerCombat.AttackState.AS_ATTACKING)
            {
                other.gameObject.GetComponent<Enemy>().AlterHealth(-30);
                other.gameObject.GetComponent<Enemy>().KnockBack(knockbackStrength);
            }
        }
    }
}
