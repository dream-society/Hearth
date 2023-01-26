using Hearth.Player;
using HNC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneTrigger : MonoBehaviour
{
    private bool interacted;
    [SerializeField] private VideoClip clip;
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        SceneTransition.TransitionFadeOutEnd += OnTransitionFadeOutEnd;
    }

    private void OnDisable()
    {
        SceneTransition.TransitionFadeOutEnd -= OnTransitionFadeOutEnd;
    }

    private void OnTransitionFadeOutEnd()
    {
        VideoPlayerManager.CutsceneStart?.Invoke(clip, sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !interacted)
        {
            interacted = true;
            SceneTransition.TransitionFadeOut?.Invoke();
        }
    }
}
