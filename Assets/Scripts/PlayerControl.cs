using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Death
{
    public class PlayerControl : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerInput.PlayerActions playerAction;
        private PlayerInput.GameActions gameSystem;
        //private PlayerControl playerControl;
        private bool gamePause;

        //camera part
        private Rigidbody rb;
        public float moveSpeed;
        public Camera mainCamera;
        private float rotationX = 0f;
        public float viewScale;
        public float sensitivityX, sensitivityY;
        public static Vector2 vector2;
        private Vector2 vector2_movement;
        private bool isAiming;

        private Animator anim;
        public float cameraMoveBack;
        public float cameraMoveDown;
        public float newVelocity;
        private float aimingMovespeed;

        // Start is called before the first frame update
        void Awake()
        {
            playerInput = new PlayerInput();
            playerAction = playerInput.Player;
            gameSystem = playerInput.Game;
            playerInput.Player.Dodge.performed += Dodge;
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            aimingMovespeed = moveSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            isAiming = Shooting.isAiming;
            vector2 = playerAction.Look.ReadValue<Vector2>();
            vector2_movement = playerAction.Movement.ReadValue<Vector2>();

            if(vector2_movement.y > 0.01)
            {
                anim.SetBool("runningForward", true);
            }    
            else
            {
                anim.SetBool("runningForward", false);
            }
            if (vector2_movement.y < -0.01)
                anim.SetBool("runningBack", true);
            else
                anim.SetBool("runningBack", false);

        }

        private void FixedUpdate()
        {
            if (isAiming)
            {
                Movement(vector2_movement, moveSpeed * 0.8f);
            }
            else
            {
                Movement(vector2_movement, moveSpeed);
            }
        }

        private void LateUpdate()
        {
            if (!isAiming)
            {
                Look(vector2);
            }
        }

        private void OnEnable()
        {
            playerInput.Player.Enable();
        }

        private void OnDisable()
        {
            playerInput.Player.Disable();
        }

        public void GamePause(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed && gamePause)
            {
                playerAction.Enable();
                gamePause = !gamePause;
            }
            else if (ctx.phase == InputActionPhase.Performed && !gamePause)
            {
                playerAction.Disable();
                gamePause = !gamePause;
            }
        }

        private void Dodge(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                if (vector2_movement.y >= 0)
                {
                    anim.SetBool("dodgingF", true);
                    mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, (mainCamera.transform.localPosition.y - cameraMoveDown), mainCamera.transform.localPosition.z);
                    Invoke("CameraBack_2", 0.8f);
                }
                    
                if (vector2_movement.y < 0)
                {
                    anim.SetBool("dodgingB", true);
                    mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, (mainCamera.transform.localPosition.z - cameraMoveBack));
                    Invoke("CameraBack_1", 1.5f);
                }
                    
            }
        }

        private void Movement(Vector2 input, float speed)
        {
            bool onStairs = GravityTest.onStairs;
            if(!onStairs)
                rb.velocity = transform.TransformDirection(input.x * speed * Time.fixedDeltaTime, newVelocity, input.y * speed * Time.fixedDeltaTime);
            else
                rb.velocity = transform.TransformDirection(input.x * speed * Time.fixedDeltaTime, newVelocity * 0.001f, input.y * speed * Time.fixedDeltaTime * 0.6f);
        }

        private void Look(Vector2 input)
        {
            //camera look up and down
            rotationX -= (input.y * Time.deltaTime) * sensitivityY;
            rotationX = Mathf.Clamp(rotationX, -viewScale, viewScale);
            //apply this to camera transform
            mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //rotate player to look left and right
            transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * sensitivityX);
        }

        private void CameraBack_1()
        {
            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, (mainCamera.transform.localPosition.z + cameraMoveBack));
        }
        private void CameraBack_2()
        {
            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, (mainCamera.transform.localPosition.y + cameraMoveDown), mainCamera.transform.localPosition.z);
        }
    }
}