using HNC;
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
        SceneTransition.gameObject.SetActive(false);
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

        Debug.Log("Open pause menu");
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
    }

    private void ResumeButtonPressed()
    {
        ClosePauseMenu();
    }

    private void SettingsButtonPressed()
    {
        OpenSettingsMenu();
    }

    private void QuitButtonPressed()
    {
        Application.Quit();
    }

    private void OpenSettingsMenu()
    {
        settingsMenu.Closed += CloseSettingsMenu;
        pauseMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);

        settingsMenu.Setup();
    }

    private void CloseSettingsMenu()
    {
        settingsMenu.Closed -= CloseSettingsMenu;
        settingsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }

    private void OnCutSceneStart(VideoClip clip, string scene)
    {
        playerUI.gameObject.SetActive(false);
    }


    private void OnCutSceneEnd()
    {
        playerUI.gameObject.SetActive(true);
        SceneTransition.gameObject.SetActive(true);
    }

}
