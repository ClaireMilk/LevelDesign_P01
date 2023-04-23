using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Death
{
    public class Shooting : MonoBehaviour
    {
        //aiming and shooting
        public float timer;
        private float currentTime;
        public float aimingSpeed;
        private int i;
        public int aimingView;
        public Camera mainCamera;
        public static bool isAiming;
        public float cameraMove_X, cameraMove_Y;
        private Animator anim;

        // Start is called before the first frame update
        void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                isAiming = true;
            }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("usingPistol", true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                isAiming = false;
                anim.SetBool("usingPistol", false);
            }

            if (Input.GetMouseButtonDown(0) && isAiming)
            {
                anim.SetTrigger("shoot");
            }
        }

        private void FixedUpdate()
        {
            if (isAiming)
            {
                currentTime += Time.fixedDeltaTime;
                if (currentTime > timer)
                {
                    if (i < aimingSpeed)
                        i++;
                    mainCamera.fieldOfView = Mathf.Lerp(60, aimingView, i / aimingSpeed);
                    mainCamera.transform.localPosition = new Vector3(cameraMove_X, cameraMove_Y, mainCamera.transform.localPosition.z);
                    currentTime = 0;
                }
            }
            else
            {
                mainCamera.fieldOfView = 60;
                i = 0;
            }
        }
    }
}
