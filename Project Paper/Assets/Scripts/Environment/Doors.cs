using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    bool closed = false;
    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        closed = true;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            if (Input.GetKeyDown("r") && !closed)
                CloseDoor();
            if (Input.GetKeyDown("r") && closed)
                OpenDoor();
            
        }

    }

    public void CloseDoor()
    {
        moving = true;
        Quaternion startRot = transform.rotation * Quaternion.Euler(0, -0, 0);
        StartCoroutine(OpenDoorAction(new Vector3(0, 130, 0), startRot, true));
    }

    public void OpenDoor()
    {
        moving = true;
        Quaternion startRot = transform.rotation * Quaternion.Euler(0, -0, 0);
        StartCoroutine(OpenDoorAction(new Vector3(0, -130, 0), startRot, false));
    }

    IEnumerator OpenDoorAction(Vector3 rot, Quaternion start, bool close)
    {
        Quaternion destination = start * Quaternion.Euler(rot);
        float startTime = Time.time;
        float percentComplete = 0f;
        while (percentComplete <= 1.0f)
        {
            percentComplete = (Time.time - startTime);
            transform.rotation = Quaternion.Slerp(start, destination, percentComplete);
            yield return null;
        }
        closed = close;
        moving = false;
        yield return null;
    }
}
