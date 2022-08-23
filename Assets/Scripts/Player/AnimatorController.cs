using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Awake() => animator = GetComponent<Animator>();
    public void StartWalkAnimation() => animator.SetBool("Walk", true);
    public void StopWalkAnimation() => animator.SetBool("Walk", false);
    public void StartRunAnimation() => animator.SetBool("Run", true);
    public void StopRunAnimation() => animator.SetBool("Run", false);

}
