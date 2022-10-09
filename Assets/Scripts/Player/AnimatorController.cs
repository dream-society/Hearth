using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator corpoAnimator;
    [SerializeField] private Animator capelliAnimator;
    [SerializeField] private Animator sciarpaAnimator;

    public void StartWalkAnimation()
    {
        corpoAnimator.SetBool("Walk", true);
        capelliAnimator.SetBool("Walk", true);
        sciarpaAnimator.SetBool("Walk", true);
    }

    public void StopWalkAnimation()
    {
        corpoAnimator.SetBool("Walk", false);
        capelliAnimator.SetBool("Walk", false);
        sciarpaAnimator.SetBool("Walk", false);
    }

    public void StartRunAnimation()
    {
        corpoAnimator.SetBool("Run", true);
        capelliAnimator.SetBool("Run", true);
        sciarpaAnimator.SetBool("Run", true);
    }

    public void StopRunAnimation()
    {
        corpoAnimator.SetBool("Run", false);
        capelliAnimator.SetBool("Run", false);
        sciarpaAnimator.SetBool("Run", false);
    }

    public void StartJumpAnimation()
    {
        corpoAnimator.SetTrigger("Jump");
        capelliAnimator.SetTrigger("Jump");
        sciarpaAnimator.SetTrigger("Jump");
    }

    public void StartFallAnimation()
    {
        corpoAnimator.SetBool("Fall", true);
        capelliAnimator.SetBool("Fall", true);
        sciarpaAnimator.SetBool("Fall", true);
    }

    public void StopFallAnimation()
    {
        corpoAnimator.SetBool("Fall", false);
        capelliAnimator.SetBool("Fall", false);
        sciarpaAnimator.SetBool("Fall", false);
    }
    public void StartLandAnimation()
    {
        corpoAnimator.SetTrigger("Land");
        capelliAnimator.SetTrigger("Land");
        sciarpaAnimator.SetTrigger("Land");
    }

    public void StartInteract()
    {
        sciarpaAnimator.SetTrigger("Interact");
    }
}
