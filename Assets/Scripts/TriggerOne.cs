using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerOne : MonoBehaviour
{
    public Transform mainCamera;
    public Transform player;
    public Text hint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hint.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hint.enabled = false;
    }
}
