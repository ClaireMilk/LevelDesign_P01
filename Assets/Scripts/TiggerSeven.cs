using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerSeven : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(door);
        }
    }
}
