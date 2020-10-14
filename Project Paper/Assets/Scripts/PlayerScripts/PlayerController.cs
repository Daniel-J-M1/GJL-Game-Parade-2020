using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float speed = 5;
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
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        Vector3 direction = input.normalized;

        Vector3 movement = direction * speed * Time.deltaTime;

        //transform.position = transform.position + movement;
        //body.MovePosition(transform.position + movement * speed);
        body.velocity = new Vector3(movement.x * speed, body.velocity.y, movement.z * speed);
    }
}
