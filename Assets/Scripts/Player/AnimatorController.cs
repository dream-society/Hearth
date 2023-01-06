using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator corpoAnimator;
    [SerializeField] private Animator capelliAnimator;
    [SerializeField] private Animator sciarpaAnimator;
    private CharacterController2D controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
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
        StopInteractAnimation();
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
        StopInteractAnimation();
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
        StopInteractAnimation();
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
        StopInteractAnimation();
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

    public void StartInteractAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        corpoAnimator.SetBool("interact", true);
        capelliAnimator.SetBool("interact", true);
        sciarpaAnimator.SetBool("interact", true);
    }

    public void StopInteractAnimation()
    {
        corpoAnimator.SetBool("interact", false);
        capelliAnimator.SetBool("interact", false);
        sciarpaAnimator.SetBool("interact", false);
    }

    public void StartSurrendAnimation()
    {
        StopIdleAnimation();
        StopMoveAnimation();
        StopJumpAnimation();
        StopLandAnimation();
        StopHitAnimation();
        StopInteractAnimation();
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
    }
}
