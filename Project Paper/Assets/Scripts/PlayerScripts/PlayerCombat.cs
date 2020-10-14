using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    float swingSpeed = 5;

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
    Vector3 baseRotation;

    // Start is called before the first frame update
    void Start()
    {
        attackState = AttackState.AS_IDLE;
        swingRadious += 10;
        baseRotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.rotation.y);
        
        float swingMultiplier = 100;
        swingMultiplier *= swingSpeed;
        
        if (attackFinished) //inputs
        {
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_ATTACK2) ||
                Input.GetKeyDown("space") && attackState == AttackState.AS_ATTACK2)
            {
                attackFinished = false;
                attackState = AttackState.AS_ATTACK3;
                comboDelayTimer = MaxComboDelay;
            }
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_ATTACK1) ||
                (Input.GetKeyDown("space") && attackState == AttackState.AS_ATTACK1))
            {
                attackFinished = false;
                attackState = AttackState.AS_ATTACK2;
                comboDelayTimer = MaxComboDelay;
            }
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_IDLE) ||
                (Input.GetKeyDown("space") && attackState == AttackState.AS_IDLE))
            {
                attackFinished = false;
                attackState = AttackState.AS_ATTACK1;
                comboDelayTimer = MaxComboDelay;
            }            
        }

        if (attackState == AttackState.AS_ATTACK3 && !attackFinished)
        {
            //rotate towards the end angle
            transform.RotateAround(transform.position, Vector3.up, swingMultiplier * Time.deltaTime);

            var targetAngle = swingRadious; //end angle
            float currAngle = transform.rotation.eulerAngles.y; //current angle
            float originAngle = player.transform.rotation.eulerAngles.y; //angle of player (so origin of our angle)     

            //clamps between 0 - 360
            var clampedTargetAngle = targetAngle + originAngle;
            if (clampedTargetAngle > 360)
                clampedTargetAngle -= 360;

            //check if its close to the target angle
            if (currAngle > clampedTargetAngle - 7 && currAngle < clampedTargetAngle + 7)
            {
                transform.rotation = player.transform.rotation;
                if (attackState == AttackState.AS_ATTACK3)
                    attackState = AttackState.AS_IDLE;
                attackFinished = true;
                comboDelayTimer = MaxComboDelay;
            }
        }
        if (attackState == AttackState.AS_ATTACK2 && !attackFinished)
        {
            //rotate towards the end angle
            transform.RotateAround(transform.position, Vector3.up, swingMultiplier * Time.deltaTime);

            var targetAngle = swingRadious; //end angle
            float currAngle = transform.rotation.eulerAngles.y; //current angle
            float originAngle = player.transform.rotation.eulerAngles.y; //angle of player (so origin of our angle)     

            //clamps between 0 - 360
            var clampedTargetAngle = targetAngle + originAngle;
            if (clampedTargetAngle > 360)
                clampedTargetAngle -= 360;

            //check if its close to the target angle
            if (currAngle > clampedTargetAngle - 10 && currAngle < clampedTargetAngle + 10)
            {
                transform.rotation = player.transform.rotation;
                if (attackState == AttackState.AS_ATTACK3)
                    attackState = AttackState.AS_IDLE;
                attackFinished = true;
                comboDelayTimer = MaxComboDelay;
            }
        }
        if (attackState == AttackState.AS_ATTACK1 && !attackFinished)
        {
            StartCoroutine(Attack1());

            /*
            //rotate towards the end angle
            transform.RotateAround(transform.position, Vector3.up, swingMultiplier * Time.deltaTime);

            var targetAngle = swingRadious; //end angle
            float currAngle = transform.rotation.eulerAngles.y; //current angle
            float originAngle = player.transform.rotation.eulerAngles.y; //angle of player (so origin of our angle)     

            //clamps between 0 - 360
            var clampedTargetAngle = targetAngle + originAngle;
            if (clampedTargetAngle > 360)
                clampedTargetAngle -= 360;

            //check if its close to the target angle
            if (currAngle > clampedTargetAngle - 7 && currAngle < clampedTargetAngle + 7)
            {
                transform.rotation = player.transform.rotation;
                if(attackState == AttackState.AS_ATTACK3)
                    attackState = AttackState.AS_IDLE;
                attackFinished = true;
                comboDelayTimer = MaxComboDelay;
            }*/
        }

        if (attackState != AttackState.AS_IDLE && attackFinished)
        {
            comboDelayTimer -= Time.deltaTime;
            if (comboDelayTimer < 0)
            {
                attackState = AttackState.AS_IDLE;
            }
        }
    }
    IEnumerator Attack1()
    {
        while (transform.rotation.y < swingRadious)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, swingRadious, 0), swingSpeed * Time.deltaTime);
            yield return null;
        }

        if (transform.rotation.y >= player.transform.rotation.y + swingRadious)
        {
            if (attackState == AttackState.AS_ATTACK3)
                attackState = AttackState.AS_IDLE;
            attackFinished = true;
            comboDelayTimer = MaxComboDelay;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        yield return null;

    }
}


