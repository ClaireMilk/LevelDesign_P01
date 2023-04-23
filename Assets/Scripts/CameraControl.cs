using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Death
{
    public class CameraControl : MonoBehaviour
    {
        private Camera mainCamera;
        private float rotationX = 0f, rotationY = 0f;
        public float viewScaleX, viewScaleY;
        public float sensitivityX, sensitivityY;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            bool isAiming = Shooting.isAiming;
            Vector2 vector2 = PlayerControl.vector2;
            if (isAiming)
            {
                Look(vector2);
                //Debug.Log(vector2);
            }
                
        }


        private void Look(Vector2 input)
        {
            rotationX -= (input.y * Time.fixedDeltaTime) * sensitivityY;
            rotationX = Mathf.Clamp(rotationX, -viewScaleX, viewScaleX);
            rotationY += (input.x * Time.fixedDeltaTime) * sensitivityX;
            rotationY = Mathf.Clamp(rotationY, -viewScaleY, viewScaleY);
            mainCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }
}
