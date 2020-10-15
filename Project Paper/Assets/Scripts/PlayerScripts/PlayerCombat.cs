using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    float swingSpeed = 0.5f;

    [SerializeField] 
    float swingRadious = 160;
    public enum AttackState
    {
        AS_IDLE,
        AS_ATTACK1,
        AS_ATTACK2,
        AS_ATTACK3
    }
    public AttackState attackState;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float MaxComboDelay = 1f;
    float comboDelayTimer;
    bool attackFinished = true;
    bool attacking = false;
    Vector3 baseRotation;

    // Start is called before the first frame update
    void Start()
    {
        attackState = AttackState.AS_IDLE;
        baseRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.rotation.y);
        
        float swingMultiplier = 100;
        swingMultiplier *= swingSpeed;
        
        if (!attacking)
        {
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_ATTACK2) ||
            Input.GetKeyDown("space") && attackState == AttackState.AS_ATTACK2)
            {
                attackState = AttackState.AS_ATTACK3;
                attacking = true;
                Quaternion startRot = player.transform.rotation * Quaternion.Euler(10, 0, -90);
                StartCoroutine(Attack(new Vector3(swingRadious * 1.9f, 0, 0), startRot));
                comboDelayTimer = MaxComboDelay;
            }
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_ATTACK1) ||
                (Input.GetKeyDown("space") && attackState == AttackState.AS_ATTACK1))
            {
                attackState = AttackState.AS_ATTACK2;
                attacking = true;
                Quaternion startRot = player.transform.rotation * Quaternion.Euler(0, 0, 70);
                StartCoroutine(Attack(new Vector3(swingRadious, 0, 0), startRot));
                comboDelayTimer = MaxComboDelay;
            }
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_IDLE) ||
                (Input.GetKeyDown("space") && attackState == AttackState.AS_IDLE))
            {
                attackState = AttackState.AS_ATTACK1;
                attacking = true;
                Quaternion startRot = player.transform.rotation * Quaternion.Euler(0, 0, -20);
                StartCoroutine(Attack(new Vector3(swingRadious, 0, swingRadious / 4), startRot));
                comboDelayTimer = MaxComboDelay;
            }

            if (attackState != AttackState.AS_IDLE)
            {
                comboDelayTimer -= Time.deltaTime;
                if (comboDelayTimer < 0)
                    attackState = AttackState.AS_IDLE;
            }
        }        

        
    }
    IEnumerator Attack(Vector3 rot, Quaternion start)
    {
        Quaternion destination = start * Quaternion.Euler(rot);
        float startTime = Time.time;
        float percentComplete = 0f;
        while (percentComplete <= 1.0f)
        {
            percentComplete = (Time.time - startTime) / swingSpeed;
            transform.rotation = Quaternion.Slerp(start, destination, percentComplete);
            yield return null;
        }

        if (attackState == AttackState.AS_ATTACK3)
            attackState = AttackState.AS_IDLE;
        transform.rotation = player.transform.rotation;
        attacking = false;
        comboDelayTimer = MaxComboDelay;
        yield return null;
    }
}