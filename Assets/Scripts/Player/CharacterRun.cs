using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;

        [Header("Move")]
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float maxSpeed = 6f;
        [SerializeField] private float speed;
        private CoyoteTime coyoteTime;
        private CharacterController2D controller;
        private Vector3 velocity;
        private float gravityScale = 1f;
        private bool isMoving;
        private bool runInput;
        private Vector2 movement;

        public int Lifes = 3;

        private AnimatorController animatorController;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce = 0f;
        [SerializeField] private float variableJumpMult = 0.5f;
        [SerializeField] private float jumpBufferTime = 0.2f;
        private bool isJumping;
        private bool jumpInput;
        private bool jumpInputStop;
        private float jumpInputStartTime;

        private bool isInAir;

        [SerializeField] private SpriteRenderer corpoSpriteRenderer;
        [SerializeField] private SpriteRenderer capelliSpriteRenderer;
        [SerializeField] private SpriteRenderer sciarpaSpriteRenderer;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            speed = walkSpeed;
            coyoteTime = GetComponent<CoyoteTime>();
            animatorController = GetComponent<AnimatorController>();
        }

        private void OnEnable()
        {
            inputHandler.move += Move;
            inputHandler.jumpPressed += JumpStart;
            inputHandler.jumpReleased += JumpEnd;
            inputHandler.runPressed += StartRun;
            inputHandler.runReleased += StopRun;
            inputHandler.interactPressed += Interact;
        }


        private void OnDisable()
        {
            inputHandler.move -= Move;
            inputHandler.jumpPressed -= JumpStart;
            inputHandler.jumpReleased -= JumpEnd;
            inputHandler.runPressed -= StartRun;
            inputHandler.runReleased -= StopRun;
            inputHandler.interactPressed -= Interact;
        }


        void Update()
        {   
            // anim

            if (controller.isGrounded && velocity.x == 0)
            {
                animatorController.StartIdleAnimation();
            }
            if (velocity.x != 0 && controller.isGrounded)
            {
                animatorController.StartMoveAnimation();
            }
            animatorController.SetYVelocity(velocity.y);
            animatorController.SetXVelocity(speed / runSpeed);


            // Apply gravity before move
            if (controller.isGrounded)
            {
                velocity.y = 0;
                if (controller.collisionState.platformBelow.GetComponent<MovingPlatform>() != null)
                {
                    transform.parent = controller.collisionState.platformBelow;
                }
                else
                {
                    transform.parent = null;
                }
            }
            else
            {
                velocity.y += -gravity * gravityScale * Time.deltaTime;
            }

            CheckVariableJump();

            if (velocity.y <= -1)
            {
                isInAir = true;
            }
            if ((controller.isGrounded || coyoteTime.Active) && jumpInput)
            {
                Jump();
            }
            if (controller.isGrounded && isInAir && velocity.y <= 0)
            {
                isInAir = false;
                animatorController.StartLandAnimation();
            }
            speed = runInput ? runSpeed : walkSpeed;
            velocity.x = movement != Vector2.zero ? movement.x * speed : 0;
            controller.move(velocity * Time.deltaTime);
            velocity = controller.velocity;

            if (velocity.x != 0)
            {
                corpoSpriteRenderer.flipX = velocity.x < 0;
                capelliSpriteRenderer.flipX = velocity.x < 0;
                sciarpaSpriteRenderer.flipX = velocity.x < 0;
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

        public void Interact()
        {
            animatorController.StartInteractAnimation();
        }

        public void GetDamaged(int dmg)
        {
            if (Lifes > 1)
            {
                Lifes -= dmg;
                PlayerUI.OnUpdateLife?.Invoke(Lifes);
                animatorController.StartHitAnimation();
            }
            else
            {
                Death();
            }
        }

        private void Death()
        {
            animatorController.StartSurrendAnimation();
        }

        public void CollectPlasticBottle(int value)
        {
            PlayerUI.OnUpdatePlasticBottle?.Invoke(value);
        }

        public void GetHealed(int heal)
        {
            if (Lifes < 3)
            {
                Lifes += heal;
                PlayerUI.OnUpdateLife?.Invoke(Lifes);
            }
        }
    }
}
