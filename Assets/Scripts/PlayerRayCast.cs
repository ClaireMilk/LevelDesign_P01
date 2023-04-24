using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Death
{
    public class PlayerRayCast : MonoBehaviour
    {
        public float rayDistance;
        public Color rayColor = Color.red;
        public Text pickipHint;
        public Text getObejct;
        private bool canPickUp;
        public static bool canCheck;
        public static bool canShow;
        private string currentObjectName;

        private void Update()
        {
            bool isChecking = PickUp.canRotate;
            if (!isChecking && !canCheck)
            {
                GameObject[] Objs;
                Objs = GameObject.FindGameObjectsWithTag("Check");
                for (int i = 0; i < Objs.Length; i++)
                {
                    Objs[i].GetComponent<PickUp>().enabled = false;
                }
            }
            bool isReading = Files.reading;
            if (!isReading && !canShow)
            {
                GameObject[] Objs;
                Objs = GameObject.FindGameObjectsWithTag("Files");
                for (int i = 0; i < Objs.Length; i++)
                {
                    Objs[i].GetComponent<Files>().enabled = false;
                }
            }

            LookRay();
        }

        private void LookRay()
        {
            //create a ray from camera
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            float trueRayDistance; //when checked items, ray distance will change

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.gameObject.tag == "CardOne")
                {
                    pickipHint.enabled = true;
                    canPickUp = true;
                }
                else if (hit.collider.gameObject.tag == "CardTwo")
                {
                    pickipHint.enabled = true;
                    canPickUp = true;
                }
                else if (hit.collider.gameObject.tag == "Check")
                {
                    pickipHint.enabled = true;
                    canCheck = true;
                    currentObjectName = hit.collider.gameObject.name;
                    hit.collider.gameObject.GetComponent<PickUp>().enabled = true;
                }
                else if (hit.collider.gameObject.tag == "Files")
                {
                    pickipHint.enabled = true;
                    canShow = true;
                    currentObjectName = hit.collider.gameObject.name;
                    hit.collider.gameObject.GetComponent<Files>().enabled = true;
                }
                else
                {
                    pickipHint.enabled = false;
                    canPickUp = false;
                    canCheck = false;
                    canShow = false;
                }

                if (canPickUp && Input.GetKey(KeyCode.E))
                {
                    getObejct.text = hit.collider.gameObject.name;
                    hit.collider.gameObject.SetActive(false);
                }

            }

            if (hit.collider == null)
            {
                trueRayDistance = rayDistance;
            }
            else
            {
                //distance between camera and items
                trueRayDistance = Vector3.Distance(transform.position, hit.collider.transform.position);
            }

            Debug.DrawRay(ray.origin, transform.forward * trueRayDistance, rayColor);

        }
    }
}

