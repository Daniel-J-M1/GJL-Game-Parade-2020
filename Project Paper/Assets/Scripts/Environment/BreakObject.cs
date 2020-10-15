using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{

    public GameObject Fractured;
    public float Shatter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            BreakAsset();
    }

    void BreakAsset()
    {
        GameObject Frac = Instantiate(Fractured, transform.position, transform.rotation);
        foreach(Rigidbody RB in Frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 Force = (RB.transform.position = transform.position).normalized * Shatter;
            RB.AddForce(Force);
        }
        Destroy(gameObject);
    }

}
