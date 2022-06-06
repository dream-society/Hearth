using System;
using System.Collections;
using UnityEngine;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;

        [Header("Move")]
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float maxSpeed = 6f;
        private CharacterController2D controller;
        private Vector3 velocity;
        private float gravityScale = 1f;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 0f;
        [SerializeField] private float variableJumpMult = 0.5f;
        [SerializeField] private float jumpBufferTime = 0.2f;
        private bool isJumping;
        private bool jumpInput;
        private bool jumpInputStop;
        private float jumpInputStartTime;

        void Awake() => controller = GetComponent<CharacterController2D>();

        private void OnEnable()
        {
            inputHandler.move += Move;
            inputHandler.jumpPressed += JumpStart;
            inputHandler.jumpReleased += JumpEnd;
        }

        private void OnDisable()
        {
            inputHandler.move -= Move;
            inputHandler.jumpPressed -= JumpStart;
            inputHandler.jumpReleased -= JumpEnd;
        }


        void Update()
        {
            // Apply gravity before move
            velocity.y += -gravity * gravityScale * Time.deltaTime;

            CheckVariableJump();

            if (controller.isGrounded && jumpInput)
            {
                Jump();
            }

            controller.move(velocity * Time.deltaTime);
            velocity = controller.velocity;
        }


        private void Move(Vector2 vel)
        {
            // velocity.x = Mathf.Clamp(vel.x * speed, -maxSpeed, maxSpeed);
            velocity.x = vel.x * speed;
        }

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
            velocity.y = Mathf.Sqrt(2f * jumpForce);
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
