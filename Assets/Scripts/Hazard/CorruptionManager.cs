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

    private void OnEnable()
    {
        CorruptionDeath += OnCorruptionDeath;
    }


    private void OnDisable()
    {
        CorruptionDeath -= OnCorruptionDeath;
    }
    private void OnCorruptionDeath()
    {
        corruptionsDeathCount++;
        if (corruptionsDeathCount == corruptions.Length)
        {
            VideoPlayerManager.CutsceneStart.Invoke(clip, sceneName);
        }
    }
}
