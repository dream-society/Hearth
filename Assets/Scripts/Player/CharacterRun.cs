using UnityEngine;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float maxSpeed = 6f;

        private Vector3 velocity;
        private float gravityScale = 1f;
        private CharacterController2D controller;

        void Awake() => controller = GetComponent<CharacterController2D>();

        private void OnEnable() => inputHandler.move += Move;

        private void OnDisable() => inputHandler.move -= Move;

        void Update()
        {
            // Apply gravity before move
            velocity.y += -gravity * gravityScale * Time.deltaTime;

            controller.move(velocity * Time.deltaTime);
            velocity = controller.velocity;
        }

        private void Move(Vector2 vel)
        {
            // velocity.x = Mathf.Clamp(vel.x * speed, -maxSpeed, maxSpeed);
            velocity.x = vel.x * speed;
        }
    }
}
