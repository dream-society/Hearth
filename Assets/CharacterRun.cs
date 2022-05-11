using UnityEngine;

namespace Hearth.Player
{
    public class CharacterRun : MonoBehaviour
    {
        public float gravity = 9.81f;

        private Vector3 velocity;
        private float gravityScale = 1f;
        private CharacterController2D controller;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
        }

        void Update()
        {
            velocity.x = 1;

            // Apply gravity before move
            velocity.y += -gravity * gravityScale * Time.deltaTime;

            controller.move(velocity * Time.deltaTime);

            velocity = controller.velocity;
        }
    }
}
