using HNC;
using RoaREngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerHandler : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void OnEnable()
    {
        SceneTransition.TransitionFadeOut += OnTransitionFadeOut;
        SceneTransition.TransitionFadeIn += OnTransitionFadeIn;
    }

    private void OnDisable()
    {
        SceneTransition.TransitionFadeOut -= OnTransitionFadeOut;
        SceneTransition.TransitionFadeIn -= OnTransitionFadeIn;
    }
    private void OnTransitionFadeOut()
    {
        RoarManager.CallFade(musicName, 2f, 0f);
    }

    private void OnTransitionFadeIn()
    {
        if (RoarManager.CallGetAudioSource(musicName) != null)
        {
            RoarManager.CallFade(musicName, 2f, 1f);
        }
        else
        {
            RoarManager.CallPlay(musicName, null);
        }
    }

}
