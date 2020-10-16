using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float speed = 30;
    Vector3 playerRotation;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void LateUpdate()
    {
        float multiplier = speed * 100;
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        Vector3 direction = input.normalized;

        Vector3 movement = direction * multiplier * Time.deltaTime;

        body.velocity = new Vector3(movement.x, body.velocity.y, movement.z);
    }
}
