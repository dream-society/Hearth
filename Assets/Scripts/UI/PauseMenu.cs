using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton = default;
    [SerializeField] private Button settingsButton = default;
    [SerializeField] private Button quitButton = default;

    public UnityAction ResumeButtonAction;
    public UnityAction SettingsButtonAction;
    public UnityAction QuitButtonAction;

    public void SetMenuScreen()
    {
        resumeButton.Select();
    }

    public void ResumePressed()
    {
        ResumeButtonAction?.Invoke();
    }

    public void SettingsPressed()
    {
        SettingsButtonAction?.Invoke();
    }

    public void QuitPressed()
    {
        QuitButtonAction?.Invoke();
    }
}
