using HNC;
using RoaREngine;
using System;
using UnityEngine;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private SceneTransition SceneTransition;

    private void OnEnable()
    {
        input.pausePressed += OpenPauseMenu;
        VideoPlayerManager.CutsceneStart += OnCutSceneStart;
        VideoPlayerManager.CutsceneEnd += OnCutSceneEnd;
    }

    private void OnDisable()
    {
        input.pausePressed -= OpenPauseMenu;
        VideoPlayerManager.CutsceneEnd -= OnCutSceneEnd;
    }

    private void Start()
    {
        SceneTransition.TransitionFadeIn?.Invoke();
        input.EnablePlayerInput();
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0.0f;
        input.pausePressed -= OpenPauseMenu;
        input.pausePressed += ClosePauseMenu;

        pauseMenu.ResumeButtonAction += ResumeButtonPressed;
        pauseMenu.SettingsButtonAction += SettingsButtonPressed;
        pauseMenu.QuitButtonAction += QuitButtonPressed;

        pauseMenu.gameObject.SetActive(true);
        pauseMenu.SetMenuScreen();
        input.EnableUIInput();

        Cursor.visible = true;
    }

    private void ClosePauseMenu()
    {
        Time.timeScale = 1.0f;
        input.pausePressed -= ClosePauseMenu;
        input.pausePressed += OpenPauseMenu;

        pauseMenu.ResumeButtonAction -= ResumeButtonPressed;
        pauseMenu.SettingsButtonAction -= SettingsButtonPressed;
        pauseMenu.QuitButtonAction -= QuitButtonPressed;

        pauseMenu.gameObject.SetActive(false);
        input.EnablePlayerInput();

        if (settingsMenu.gameObject.activeInHierarchy)
        {
            settingsMenu.gameObject.SetActive(false);
        }
    }

    private void ResumeButtonPressed()
    {
        ClosePauseMenu();
        RoarManager.CallPlay("UI", null);
    }

    private void SettingsButtonPressed()
    {
        OpenSettingsMenu();
        RoarManager.CallPlay("UI", null);
    }

    private void QuitButtonPressed()
    {
        Application.Quit();
    }

    private void OpenSettingsMenu()
    {
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.activeInHierarchy);
    }

    private void OnCutSceneStart(VideoClip clip, string scene)
    {
        playerUI.gameObject.SetActive(false);
    }

    private void OnCutSceneEnd()
    {
        playerUI.gameObject.SetActive(true);
        SceneTransition.gameObject.SetActive(true);
        SceneTransition.TransitionFadeIn.Invoke();
    }

}
