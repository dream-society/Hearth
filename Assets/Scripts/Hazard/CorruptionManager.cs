using HNC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CorruptionManager : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    [SerializeField] private string sceneName;
    [SerializeField] Transform[] corruptions;

    public static Action CorruptionDeath;
    private int corruptionsDeathCount;

    public bool skip;

    private void Update()
    {
        if (skip)
        {
            skip = false;
            SceneTransition.TransitionFadeOut?.Invoke();
        }
    }

    private void OnEnable()
    {
        CorruptionDeath += OnCorruptionDeath;
        SceneTransition.TransitionFadeOutEnd += StartCutScene;
    }

    private void OnDisable()
    {
        CorruptionDeath -= OnCorruptionDeath;
        SceneTransition.TransitionFadeOutEnd -= StartCutScene;
    }

    private void StartCutScene()
    {
        VideoPlayerManager.CutsceneStart.Invoke(clip, sceneName);
    }

    private void OnCorruptionDeath()
    {
        corruptionsDeathCount++;
        if (corruptionsDeathCount == corruptions.Length)
        {
            SceneTransition.TransitionFadeOut?.Invoke();
        }
    }
}
