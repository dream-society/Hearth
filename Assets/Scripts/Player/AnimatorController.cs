using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    void Awake() => animator = GetComponent<Animator>();
    public void StartWalkAnimation() => animator.SetBool("Walk", true);
    public void StopWalkAnimation() => animator.SetBool("Walk", false);
    public void StartRunAnimation() => animator.SetBool("Run", true);
    public void StopRunAnimation() => animator.SetBool("Run", false);
    public void StartJumpAnimation() => animator.SetTrigger("Jump");
    public void StartFallAnimation() => animator.SetBool("Fall", true);
    public void StopFallAnimation() => animator.SetBool("Fall", false);
    public void StartLandAnimation() => animator.SetTrigger("Land");


}
