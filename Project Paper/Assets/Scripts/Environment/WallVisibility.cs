using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility : MonoBehaviour
{
    public GameObject Wall1;
    public GameObject Wall2;

    //GameObject Player;
    public CapsuleCollider Object;


    void OnTriggerEnter(Collider other)
    {
       if (other == Object)
       {
            //Wall1.GetComponent<MeshRenderer>().enabled = false;
            Wall1.GetComponent<MeshRenderer>().material.color = new Color (1f, 1.0f, 1.0f, 0.5f);
            Wall2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1.0f, 1.0f, 0.5f);
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == Object)
        {
            Wall1.GetComponent<MeshRenderer>().material.color = new Color(1f, 1.0f, 1.0f, 1f);
            Wall2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1.0f, 1.0f, 1f);
        }
    }
}
