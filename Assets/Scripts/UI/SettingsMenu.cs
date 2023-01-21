using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private GameObject firstElement; // Required by EventSystem


    public UnityAction Closed;

    private void OnEnable()
    {
        input.pausePressed += CloseScreen;
    }

    private void OnDisable()
    {
        input.pausePressed -= CloseScreen;
    }

    private void CloseScreen()
    {
        Closed?.Invoke();
    }

    public void Setup()
    {
        EventSystem.current.SetSelectedGameObject(firstElement);
    }

    public void ChangeMasterVolume(float volume)
    {
        // TODO
    }
    public void ChangeMusicVolume(float volume)
    {
        // TODO
    }
    public void ChangeSFXVolume(float volume)
    {
        // TODO
    }
}
