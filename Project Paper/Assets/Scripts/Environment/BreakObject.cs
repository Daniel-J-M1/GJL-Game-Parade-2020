using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{

    public GameObject Fractured;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            BreakAsset();
    }

    void BreakAsset()
    {
        Instantiate(Fractured, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
