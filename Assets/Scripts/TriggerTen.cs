using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTen : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.localPosition = new Vector3(player.transform.localPosition.x, (player.transform.localPosition.y + 4), player.transform.localPosition.z);
        }
    }
}
