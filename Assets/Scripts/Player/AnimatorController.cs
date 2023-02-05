using HNC;
using System;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator corpoAnimator;
    [SerializeField] private Animator capelliAnimator;
    [SerializeField] private Animator sciarpaAnimator;
    private CharacterController2D controller;
    private PlayerPowerManagement ppm;
    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        ppm = GetComponent<PlayerPowerManagement>();
    }

    private void OnEnable()
    {
        SceneTransition.TransitionFadeOut += OnTransitionFadeOut;
    }


    private void OnDisable()
    {
        SceneTransition.TransitionFadeOut -= OnTransitionFadeOut;
    }
    private void OnTransitionFadeOut()
    {
        StartIdleAnimation();
    }

    public void StartIdleAnimation()
    {
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("idle", true);
        capelliAnimator.SetBool("idle", true);
        sciarpaAnimator.SetBool("idle", true);
    }

    public void StopIdleAnimation()
    {
        corpoAnimator.SetBool("idle", false);
        capelliAnimator.SetBool("idle", false);
        sciarpaAnimator.SetBool("idle", false);
    }

    public void StartMoveAnimation()
    {
        StopIdleAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("move", true);
        capelliAnimator.SetBool("move", true);
        sciarpaAnimator.SetBool("move", true);
    }

    public void StopMoveAnimation()
    {
        corpoAnimator.SetBool("move", false);
        capelliAnimator.SetBool("move", false);
        sciarpaAnimator.SetBool("move", false);
    }

    public void StartJumpAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("inAir", true);
        capelliAnimator.SetBool("inAir", true);
        sciarpaAnimator.SetBool("inAir", true);
    }

    public void StopJumpAnimation()
    {
        corpoAnimator.SetBool("inAir", false);
        capelliAnimator.SetBool("inAir", false);
        sciarpaAnimator.SetBool("inAir", false);
    }

    public void StartLandAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        corpoAnimator.SetBool("land", true);
        capelliAnimator.SetBool("land", true);
        sciarpaAnimator.SetBool("land", true);
    }

    public void StopLandAnimation()
    {
        corpoAnimator.SetBool("land", false);
        capelliAnimator.SetBool("land", false);
        sciarpaAnimator.SetBool("land", false);
    }

    public void StartHitAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("hit", true);
        capelliAnimator.SetBool("hit", true);
        sciarpaAnimator.SetBool("hit", true);
    }

    public void StopHitAnimation()
    {
        corpoAnimator.SetBool("hit", false);
        capelliAnimator.SetBool("hit", false);
        sciarpaAnimator.SetBool("hit", false);
    }

    public void StartSurrendAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        StopHitAnimation();
        corpoAnimator.SetBool("surrend", true);
        capelliAnimator.SetBool("surrend", true);
        sciarpaAnimator.SetBool("surrend", true);
    }

    public void StopSurrendAnimation()
    {
        corpoAnimator.SetBool("surrend", false);
        capelliAnimator.SetBool("surrend", false);
        sciarpaAnimator.SetBool("surrend", false);
    }

    public void StartFlyAnimation() 
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("fly", true);
        capelliAnimator.SetBool("fly", true);
        sciarpaAnimator.SetBool("fly", true);
    }

    public void StopFlyAnimation()
    {
        corpoAnimator.SetBool("fly", false);
        capelliAnimator.SetBool("fly", false);
        sciarpaAnimator.SetBool("fly", false);
        corpoAnimator.SetBool("inAir", true);
        capelliAnimator.SetBool("inAir", true);
        sciarpaAnimator.SetBool("inAir", true);
    }

    public void SetYVelocity(float yVelocity)
    {
        corpoAnimator.SetFloat("yVelocity", yVelocity);
        capelliAnimator.SetFloat("yVelocity", yVelocity);
        sciarpaAnimator.SetFloat("yVelocity", yVelocity);
    }

    public void SetXVelocity(float xVelocity)
    {
        corpoAnimator.SetFloat("xVelocity", xVelocity);
        capelliAnimator.SetFloat("xVelocity", xVelocity);
        sciarpaAnimator.SetFloat("xVelocity", xVelocity);
    }

    private void Update()
    {
        if (!corpoAnimator.GetBool("hit") && !corpoAnimator.GetBool("surrend"))
        {
            if (controller.isGrounded)
            {
                if (controller.velocity.x == 0)
                {
                    StartIdleAnimation();
                }
                else
                {
                    StartMoveAnimation();
                }
            }
            if (controller.isGrounded && !controller.collisionState.wasGroundedLastFrame)
            {
                StartLandAnimation();
            }
            else
            {
                SetYVelocity(controller.velocity.y);
            }
        }

        if (ppm.IsOnGazzaForm && !controller.isGrounded)
        {
            StartFlyAnimation();

        }
        else
        {
            if (corpoAnimator.GetBool("fly"))
            {
                StopFlyAnimation();
            }
        }
    }
}
