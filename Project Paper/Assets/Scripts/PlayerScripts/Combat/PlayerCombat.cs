using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    float swingSpeed = 0.5f;

    [SerializeField] 
    float swingRadious = 160;
    public enum AttackState
    {
        AS_IDLE,
        AS_ATTACK1,
        AS_ATTACK2,
        AS_ATTACK3,
        AS_SECONDARY
    }
    public AttackState attackState;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float MaxComboDelay = 0.8f;
    float comboDelayTimer;
    bool attackFinished = true;
    bool attacking = false;
    Vector3 baseRotation;

    GameObject penTip;
    bool spraying = false;
    float maxSprayAmmo = 100;
    float sprayAmmo;

    // Start is called before the first frame update
    void Start()
    {
        attackState = AttackState.AS_IDLE;
        baseRotation = new Vector3(0, 0, 0);
        sprayAmmo = maxSprayAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        penTip = GameObject.FindGameObjectWithTag("PenTip");
        float swingMultiplier = 100;
        swingMultiplier *= swingSpeed;
        
        if(Input.GetMouseButton(1) && !spraying && sprayAmmo > 0)
        {
            spraying = true;
            Quaternion startRot = player.transform.rotation * Quaternion.Euler(0, 0, 0);
            StartCoroutine(SprayArm(new Vector3(90, 0, 0), startRot));

        }

        if (!attacking && !Input.GetMouseButton(1))
        {
            if ((Input.GetMouseButtonDown(0) && attackState == AttackState.AS_ATTACK2) ||
            Input.GetKeyDown("space") && attackState == AttackState.AS_ATTACK2)
            {
                attackState = AttackState.AS_ATTACK3;
                attacking = true;
                Quaternion startRot = player.transform.rotation * Quaternion.Euler(10, 0, -90);
                float radClamp = swingRadious * 1.5f;
                if (radClamp >= 180)
                    radClamp = 179;
                StartCoroutine(Attack(new Vector3(radClamp, 0, 0), startRot));
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

    IEnumerator Spray()
    {
        float timer = 0.02f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (sprayAmmo > 0)
        {
            Instantiate(projectile, penTip.transform.position, Quaternion.identity);
            SetAmmo(-1);
            if (Input.GetMouseButton(1))
            {
                StartCoroutine(Spray());
            }
            else
            {
                transform.rotation = Quaternion.Euler(baseRotation);
                spraying = false;
            }
            yield return null;
        }
        else
        {
            transform.rotation = Quaternion.Euler(baseRotation);
            spraying = false;
        }
        yield return null;
    }

    IEnumerator SprayArm(Vector3 rot, Quaternion start)
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
        if (sprayAmmo > 0 && Input.GetMouseButton(1))
        {
            transform.rotation = player.transform.rotation * Quaternion.Euler(rot);
            StartCoroutine(Spray());
            yield return null;
        }
        if (sprayAmmo <= 0 || !Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(baseRotation);
            spraying = false;
            yield return null;
        }
        yield return null;
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

    public void SetAmmo(float amount)
    {
        sprayAmmo += amount;
    }
}