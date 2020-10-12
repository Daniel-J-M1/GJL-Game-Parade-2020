using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal;

        horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 315, 0);
        }
    }
}
