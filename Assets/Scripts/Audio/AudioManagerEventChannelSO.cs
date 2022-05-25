using UnityEngine;
using UnityEngine.Events;

namespace DS.Audio
{
    [CreateAssetMenu(fileName = "AudioManagerEventChannel", menuName = "DreamSociety/Audio/Audio Manager Event Channel")]
    public class AudioManagerEventChannelSO : ScriptableObject
    {
        #region delegates
        public delegate SoundEmitterKey SoundBankPlayAction(
            SoundBankSO audioClipsBank,
            AudioConfigurationSO configuration,
            Vector3 position
        );
        public delegate bool SoundBankStopAction(SoundEmitterKey key);
        public delegate bool SoundBankFinishAction(SoundEmitterKey key);
        public delegate void SoundBankPauseAction(SoundEmitterKey key);
        public delegate void SoundBankResumeAction(SoundEmitterKey key);
        public delegate SoundEmitterKey MusicPlayAction(
            SoundBankSO audioClipsBank,
            AudioConfigurationSO configuration,
            float fadeTime,
            float initialVolume
        );
        #endregion

        public MusicPlayAction OnMusicPlayRequested;
        public SoundBankStopAction OnMusicStopRequested;
        public UnityAction<SoundEmitterKey, int, float> OnMusicFadeLayerIn;
        public UnityAction<SoundEmitterKey, int, float> OnMusicFadeLayerOut;

        public SoundBankPlayAction OnSFXPlayRequested;
        public SoundBankStopAction OnSFXStopRequested;
        public SoundBankFinishAction OnSFXFinishRequested;
        public SoundBankPauseAction OnSFXPauseRequested;
        public SoundBankResumeAction OnSFXResumeRequested;

        public UnityAction<float> OnMasterVolumeChanged;
        public UnityAction<float> OnMusicVolumeChanged;
        public UnityAction<float> OnSFXVolumeChanged;

        public UnityAction<string> OnChangeAudioMixerSnapshot;

        public SoundEmitterKey RaisePlayMusic(SoundBankSO audioClipsBank, AudioConfigurationSO conf, float fadeTime = 0f, float initialVolume = 0f) =>
            OnMusicPlayRequested?.Invoke(audioClipsBank, conf, fadeTime, initialVolume);
        public void RaiseStopMusic(SoundEmitterKey key) => OnMusicStopRequested?.Invoke(key);
        public void RaiseFadeMusicLayerIn(SoundEmitterKey key, int layer, float fadeTime) => OnMusicFadeLayerIn?.Invoke(key, layer, fadeTime);
        public void RaiseFadeMusicLayerOut(SoundEmitterKey key, int layer, float fadeTime) => OnMusicFadeLayerOut?.Invoke(key, layer, fadeTime);

        public SoundEmitterKey RaisePlaySFX(SoundBankSO audioClipsBank, AudioConfigurationSO conf, Vector3 position) =>
            OnSFXPlayRequested?.Invoke(audioClipsBank, conf, position);
        public void RaiseStopSFX(SoundEmitterKey key) => OnSFXStopRequested?.Invoke(key);
        public void RaiseFinishSFX(SoundEmitterKey key) => OnSFXFinishRequested?.Invoke(key);
        public void RaisePauseSFX(SoundEmitterKey key) => OnSFXPauseRequested?.Invoke(key);
        public void RaiseResumeSFX(SoundEmitterKey key) => OnSFXResumeRequested?.Invoke(key);

        public void RaiseMasterVolumeChanged(float volume) => OnMasterVolumeChanged?.Invoke(volume);
        public void RaiseMusicVolumeChanged(float volume) => OnMusicVolumeChanged?.Invoke(volume);
        public void RaiseSFXVolumeChanged(float volume) => OnSFXVolumeChanged?.Invoke(volume);

        public void RaiseChangeAudioMixerSnapshot(string snapshotName) => OnChangeAudioMixerSnapshot?.Invoke(snapshotName);
    }

}

