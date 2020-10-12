using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float swingSpeed = 5;
    public float swingRadious = 160;
    float angleSwung = 0;
    enum AttackState
    {
        AS_IDLE,
        AS_ATTACKING
    }

    AttackState attackState;
    public GameObject player;
    Transform currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        attackState = AttackState.AS_IDLE;
        swingRadious += 10;
    }

    // Update is called once per frame
    void Update()
    {
        float swingMultiplier = 100;
        swingMultiplier *= swingSpeed;


        //left click
        if (Input.GetMouseButtonDown(0) && attackState == AttackState.AS_IDLE)
        {
            attackState = AttackState.AS_ATTACKING;
        }

        if (attackState == AttackState.AS_ATTACKING)
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
                attackState = AttackState.AS_IDLE;
                angleSwung = 0;
            }
        }
    }

    float ClampAngle(float angle)
    {
        float returnAngle = angle;
        if (angle < 0)
            returnAngle = (angle + (360 * ((angle / 360) + 1)));
        else if (angle > 360)
            returnAngle = (angle - (360 * (angle / 360)));
        else if (returnAngle == 360)
            returnAngle = 0;

        return returnAngle;
    }
}
