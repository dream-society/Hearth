using HNC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public static UnityAction<VideoClip, string> CutsceneStart;
    public static UnityAction CutsceneEnd;

    private VideoPlayer player;
    private string sceneName;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        CutsceneStart += OnCutsceneStart;
    }

    private void OnCutsceneStart(VideoClip clip, string scene)
    {
        sceneName = scene;

        player.enabled = true;
        player.clip = clip;
        player.loopPointReached += EndReached;
        player.Play();
    }

    private void EndReached(VideoPlayer source)
    {
        player.enabled = false;

        CutsceneEnd?.Invoke();

        if (sceneName == "")
        {
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
