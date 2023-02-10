using HNC;
using RoaREngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform menu;
    [SerializeField] RectTransform settingsMenu;
    [SerializeField] private VideoClip clip;
    [SerializeField] private string sceneName;
    [SerializeField] private string musicName;
    [SerializeField] private InputHandler input;
    private void Start()
    {
        input.EnableUIInput();
        Cursor.visible= true;
        SceneTransition.TransitionFadeIn?.Invoke();
        RoarManager.CallPlay(musicName, null);
    }

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
        menu.gameObject.SetActive(false);
        VideoPlayerManager.CutsceneStart.Invoke(clip, sceneName);
    }
    public void LoadPlayScene()
    {
        Cursor.visible = false;
        RoarManager.CallStop(musicName, null);
        SceneTransition.TransitionFadeOut?.Invoke();
        RoarManager.CallPlay("UI", null);
    }

    public void ToggleSettings()
    {
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.activeInHierarchy);
        RoarManager.CallPlay("UI", null);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
