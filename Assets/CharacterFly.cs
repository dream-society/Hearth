using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hearth.Player
{
    public class CharacterFly : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float speed = 6f;
        [SerializeField] private SpriteRenderer corpoSpriteRenderer;
        [SerializeField] private SpriteRenderer capelliSpriteRenderer;
        [SerializeField] private SpriteRenderer sciarpaSpriteRenderer;

        private CharacterController2D controller;
        private Vector2 movement;
        private Vector3 velocity;
        private int gravityScale = 1;

        private void Awake()
        {
            controller = GetComponent<CharacterController2D>();
        }

        private void OnEnable()
        {
            inputHandler.move += Move;
        }


        private void OnDisable()
        {
            inputHandler.move -= Move;
        }

        private void Update()
        {
            if (movement.y <= 0)
            {
                velocity.y += -gravity * gravityScale * Time.deltaTime;
            }
            else
            {
                velocity.y = movement.y * speed;
            }

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
    }
}
