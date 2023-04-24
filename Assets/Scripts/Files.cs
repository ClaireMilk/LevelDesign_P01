using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Death
{
    public class Files : MonoBehaviour
    {
        public static bool isPause;
        public static bool reading;
        public GameObject content;

        private void Update()
        {
            bool canRead = PlayerRayCast.canShow;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(canRead && !reading)
                {
                    content.SetActive(true);
                    isPause = true;
                    reading = true;
                }
                else
                {
                    content.SetActive(false);
                    isPause = false;
                    reading = false;
                }
            }
        }
    }
}
