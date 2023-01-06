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
        public bool isJumping;
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
            velocity.y += -gravity * gravityScale * Time.deltaTime;

            // anim
            if (controller.isGrounded)
            {
                if (velocity.x == 0)
                {
                    //animatorController.StartIdleAnimation();
                }
                else
                {
                    //animatorController.StartMoveAnimation();
                }
            }
            animatorController.SetXVelocity(speed / runSpeed);


            // Apply gravity before move
            if (controller.isGrounded)
            {
                if (controller.collisionState.platformBelow.GetComponent<MovingPlatform>() != null)
                {
                    transform.parent = controller.collisionState.platformBelow;
                }
            }
            else
            {
                //isInAir = true;
                //velocity.y += -gravity * gravityScale * Time.deltaTime;
                //animatorController.SetYVelocity(velocity.y);
            }

            CheckVariableJump();

            if ((controller.isGrounded || coyoteTime.Active) && jumpInput)
            {
                Jump();
            }
            if (controller.isGrounded && !controller.collisionState.wasGroundedLastFrame)
            {
                //animatorController.StartLandAnimation();
            }
            speed = runInput ? runSpeed : walkSpeed;
            velocity.x = movement != Vector2.zero ? movement.x * speed : 0;
            controller.move(velocity * Time.deltaTime);
            velocity = controller.velocity;

            CheckFlip();
        }

        private void CheckFlip()
        {
            if (movement.x != 0)
            {
                corpoSpriteRenderer.flipX = movement.x < 0;
                capelliSpriteRenderer.flipX = movement.x < 0;
                sciarpaSpriteRenderer.flipX = movement.x < 0;
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
            transform.parent = null;
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
            Lifes -= dmg;
            PlayerUI.OnUpdateLife?.Invoke(Lifes);

            if (Lifes <= 0)
            {
                Death();
            }
            else
            {
                animatorController.StartHitAnimation();
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
