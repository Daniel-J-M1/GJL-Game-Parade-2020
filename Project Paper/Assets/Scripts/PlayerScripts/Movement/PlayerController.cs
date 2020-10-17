using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float speed = 10;
    Vector3 playerRotation;
    Rigidbody body;
    PlayerCombat playerCombat;
    bool dashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();
        body = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown("left shift"))
        {
            dashing = true;
            StartCoroutine(Dash(movement));
        }

        if (!dashing)
            body.velocity = movement * Time.deltaTime;
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
}
