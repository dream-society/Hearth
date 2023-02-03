using HNC;
using RoaREngine;
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
        RoarManager.CallSetAudioMixerVolumeWithSlider("AudioMixer", "MasterVolume", volume);
    }
    public void ChangeMusicVolume(float volume)
    {
        RoarManager.CallSetAudioMixerVolumeWithSlider("AudioMixer", "MusicVolume", volume);

    }
    public void ChangeSFXVolume(float volume)
    {
        RoarManager.CallSetAudioMixerVolumeWithSlider("AudioMixer", "SFXVolume", volume);

    }
}
