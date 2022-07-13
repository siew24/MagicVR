using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTeleportationArea : MonoBehaviour
{
    public BaseGameEvent onTeleportPlatformEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onTeleportPlatformEvent.Raise();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onTeleportPlatformEvent.Raise();
        }
    }
}
