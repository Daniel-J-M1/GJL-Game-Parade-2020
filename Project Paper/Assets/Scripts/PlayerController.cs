using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector3 playerRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal;
        float vertical;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
    }
}
