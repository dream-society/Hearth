using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
            VideoPlayerManager.CutsceneStart += OnCutSceneStart;
        }

        private void OnDisable()
        {
            inputHandler.move -= Move;
            inputHandler.jumpPressed -= JumpStart;
            inputHandler.jumpReleased -= JumpEnd;
            inputHandler.runPressed -= StartRun;
            inputHandler.runReleased -= StopRun;
            inputHandler.interactPressed -= Interact;
            VideoPlayerManager.CutsceneStart -= OnCutSceneStart;
        }

        private void OnCutSceneStart(VideoClip arg0, string arg1)
        {
            speed = 0;
            controller.velocity = Vector3.zero;
            velocity = Vector3.zero;
            movement = Vector2.zero;
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
            GetComponent<PlayerPowerManagement>().Interact();
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
    }
}
