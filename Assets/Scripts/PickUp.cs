using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Death
{
    public class PickUp : MonoBehaviour
    {
        private bool canRotate;
        public static bool isPause;

        //pickup and put back
        public Transform cameraTransform;
        public Transform itemsTransform;
        public Vector3 vector;
        private Rigidbody rb;
        private Vector3 originalVector_p;
        private Vector3 originalVector_r;

        //rotate object
        public int yMinLimit;
        public int yMaxLimit;
        public float xSpeed;
        public float ySpeed;
        protected float x, y;

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            isPause = false;
            canRotate = false;
            originalVector_p = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            originalVector_r = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        // Update is called once per frame
        void Update()
        {
            bool canPickUp = PlayerRayCast.canCheck;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canPickUp && !canRotate)
                {
                    transform.parent = cameraTransform;
                    transform.localPosition = new Vector3(vector.x, vector.y, vector.z);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    rb.isKinematic = true;
                    isPause = true;
                    canRotate = true;
                }
                else
                {
                    isPause = false;
                    transform.parent = itemsTransform;
                    transform.localRotation = Quaternion.Euler(originalVector_r.x, originalVector_r.y, originalVector_r.z);
                    transform.localPosition = new Vector3(originalVector_p.x, originalVector_p.y, originalVector_p.z);
                    canRotate = false;
                }
            }

            MouseControlObjectRotation();
        }

        private void MouseControlObjectRotation()
        {
            if (Input.GetMouseButton(0) && canRotate)
            {
                //get mouse x movement value
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                y = ClampAngle(y, yMinLimit, yMaxLimit);
                Quaternion rotation = Quaternion.Euler(y, -x, 0);
                transform.rotation = rotation;
            }
        }

        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
