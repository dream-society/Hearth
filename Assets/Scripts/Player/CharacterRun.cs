using System.Collections;
using UnityEngine;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private AnimatorController animatorController;

        [Header("Move")]
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float maxSpeed = 6f;
        [SerializeField] private float speed;
        private CharacterController2D controller;
        private Vector3 velocity;
        private float gravityScale = 1f;
        private bool isMoving;
        private bool runInput;
        private Vector2 movement;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 0f;
        [SerializeField] private float variableJumpMult = 0.5f;
        [SerializeField] private float jumpBufferTime = 0.2f;
        private bool isJumping;
        private bool jumpInput;
        private bool jumpInputStop;
        private float jumpInputStartTime;

        private bool isInAir;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            speed = walkSpeed;
        }

        private void OnEnable()
        {
            inputHandler.move += Move;
            inputHandler.jumpPressed += JumpStart;
            inputHandler.jumpReleased += JumpEnd;
            inputHandler.runPressed += StartRun;
            inputHandler.runReleased += StopRun;
        }


        private void OnDisable()
        {
            inputHandler.move -= Move;
            inputHandler.jumpPressed -= JumpStart;
            inputHandler.jumpReleased -= JumpEnd;
            inputHandler.runPressed -= StartRun;
            inputHandler.runReleased -= StopRun;
        }


        void Update()
        {
            // Apply gravity before move
            velocity.y += -gravity * gravityScale * Time.deltaTime;

            CheckVariableJump();

            if (velocity.y <= -1)
            {
                animatorController.StartFallAnimation();
                isInAir = true;
            }
            if (controller.isGrounded && jumpInput)
            {
                Jump();
            }
            if (controller.isGrounded && isInAir && velocity.y <= 0)
            {
                isInAir = false;
                animatorController.StopFallAnimation();
                animatorController.StartLandAnimation();
            }
            speed = runInput ? runSpeed : walkSpeed;
            velocity.x = movement != Vector2.zero ? movement.x * speed : 0;
            controller.move(velocity * Time.deltaTime);
            velocity = controller.velocity;

            if (velocity.x != 0)
            {
                spriteRenderer.flipX = velocity.x < 0;
                if (speed == walkSpeed)
                {
                    animatorController.StartWalkAnimation();
                    animatorController.StopRunAnimation();
                }
                else if (speed == runSpeed)
                {
                    animatorController.StartRunAnimation();
                }
            }
            else
            {
                animatorController.StopRunAnimation();
                animatorController.StopWalkAnimation();
            }
        }

        private void Move(Vector2 move)
        {
            movement = move;
        }

        private void StartRun() => runInput = true;

        private void StopRun() => runInput = false;

        private void JumpStart()
        {
            jumpInput = true;
            StartCoroutine(JumpBufferClear());
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        private void Jump()
        {
            isJumping = true;
            isInAir = true;
            velocity.y = Mathf.Sqrt(2f * jumpForce);
            animatorController.StartJumpAnimation();
        }

        private IEnumerator JumpBufferClear()
        {
            yield return new WaitForSeconds(jumpBufferTime);
            jumpInput = false;
        }

        private void JumpEnd()
        {
            jumpInputStop = true;
        }

        private void CheckVariableJump()
        {
            if (!isJumping)
            {
                return;
            }
            if (jumpInputStop)
            {
                velocity.y *= variableJumpMult;
                isJumping = false;
            }
            else if (velocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }
}
