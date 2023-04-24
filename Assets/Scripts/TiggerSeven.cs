using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerSeven : MonoBehaviour
{
    public GameObject door;
    public GameObject metalDoor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(door);
            Destroy(metalDoor);
        }
    }
}
