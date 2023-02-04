using HNC;
using RoaREngine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        public static Action EnablePlayerInput;
        public static Action DisablePlayerInput;

        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private SaveSystem saveSystem;

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
        private int plasticBottles = 0;
        public int PlasticBottles { get => plasticBottles; }

        private PlayerPowerManagement ppm;

        public bool CanInteract;

        private IInteractable interactable;

        bool gamePaused = false;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            speed = walkSpeed;
            coyoteTime = GetComponent<CoyoteTime>();
            animatorController = GetComponent<AnimatorController>();
            ppm = GetComponent<PlayerPowerManagement>();
        }

        private void OnEnable()
        {
            inputHandler.move += Move;
            inputHandler.jumpPressed += JumpStart;
            inputHandler.jumpReleased += JumpEnd;
            inputHandler.runPressed += StartRun;
            inputHandler.runReleased += StopRun;
            inputHandler.interactPressed += Interact;
            inputHandler.pausePressed += TogglePause;
            EnablePlayerInput += TogglePause;
            SceneTransition.TransitionFadeOut += OnTransitionFadeOut;
        }

        public void TogglePause()
        {
            gamePaused = !gamePaused;
        }

        private void OnDisable()
        {
            inputHandler.move -= Move;
            inputHandler.jumpPressed -= JumpStart;
            inputHandler.jumpReleased -= JumpEnd;
            inputHandler.runPressed -= StartRun;
            inputHandler.runReleased -= StopRun;
            inputHandler.interactPressed -= Interact;
            inputHandler.pausePressed += TogglePause;
            EnablePlayerInput += TogglePause;
            SceneTransition.TransitionFadeOut -= OnTransitionFadeOut;
        }

        private void OnTransitionFadeOut()
        {
            speed = 0;
            controller.velocity.x = 0;
            velocity.x = 0;
            movement.x = 0;
            enabled = false;
        }

        private void Start()
        {
            if (saveSystem.SaveData.Player.Position != Vector3.zero)
            {
                transform.position = saveSystem.SaveData.Player.Position;
            }
        }

        void Update()
        {
            if (gamePaused)
            {
                return;
            }

            velocity.y += -gravity * gravityScale * Time.deltaTime;

            animatorController.SetXVelocity(speed / runSpeed);


            // Apply gravity before move
            if (controller.isGrounded)
            {
                if (controller.collisionState.platformBelow.GetComponent<MovingPlatform>() != null)
                {
                    transform.parent = controller.collisionState.platformBelow;
                } 
            }

            CheckVariableJump();

            if ((controller.isGrounded || coyoteTime.Active) && jumpInput)
            {
                Jump();
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
            RoarManager.CallPlay("Jump", null);
            jumpInput = true;
            StartCoroutine(JumpBufferClear());
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        private void Jump()
        {
            float parentVelocity = 1;
            if (transform.parent != null)
            {
                parentVelocity = 1.5f;
            }
            velocity.y = Mathf.Sqrt(2f * jumpForce) * parentVelocity;
            transform.parent = null;
            isJumping = true;
            isInAir = true;
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
            if (CanInteract)
            {
                interactable.Interact();
                ppm.Interact();
            }
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

        public void EndDeath()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void CollectPlasticBottle(int value)
        {
            plasticBottles++;
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

        public void PlayFootstepSFX()
        {
            RoarManager.CallPlay("Footsteps", null);
        }

        public void PlayHurtSFX()
        {
            RoarManager.CallPlay("Hurt", null);
        }

        public void PlaySurrendSFX()
        {
            RoarManager.CallPlay("Surrend", null);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<LeverInteractable>() != null)
            {
                if (!collision.GetComponent<LeverInteractable>().Interacted)
                {
                    CanInteract = true;
                    interactable = collision.GetComponent<LeverInteractable>();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision is IInteractable)
            {
                CanInteract = false;
            }
        }
    }
}
