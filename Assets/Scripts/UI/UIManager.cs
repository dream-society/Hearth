using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SettingsMenu settingsMenu;

    private void OnEnable()
    {
        input.pausePressed += OpenPauseMenu;
    }

    private void OnDisable()
    {
        input.pausePressed -= OpenPauseMenu;
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
}
