using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerFive : MonoBehaviour
{
    public Text cardText;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && cardText.text == "员工卡（二级）")
        {
            Destroy(door);
        }
    }
}
