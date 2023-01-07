using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciarpaChain : MonoBehaviour
{
    [SerializeField] private Animator sciarpaAnimator;


    public void SetSciarpaInteractFalse()
    {
        sciarpaAnimator.SetBool("interact", false);
    }
}
