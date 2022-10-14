using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutosaveUI : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAutosaveAnimation()
    {
        animator.SetTrigger("Start");
    }
}
