using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.Audio;

namespace DS.Audio
{
    [RequireComponent(typeof(Pooler))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer = default;
        [SerializeField] private AudioMixerSnapshot[] audioMixerSnapshots = default;
        [Range(0f, 1f)] private float masterVolume = 1f;
        [Range(0f, 1f)] private float musicVolume = 1f;
        [Range(0f, 1f)] private float sfxVolume = 1f;

        [SerializeField] private AudioManagerEventChannelSO audioManagerEventChannel;
        private Pooler pooler;

        private SoundEmitterMap soundEmitterMap;
        private SoundEmitterMap musicMap;
        private SoundEmitterKey musicKey = SoundEmitterKey.Invalid;

        private void OnEnable()
        {
            audioManagerEventChannel.OnMusicPlayRequested += PlayMusic;
            audioManagerEventChannel.OnMusicStopRequested += StopMusic;
            audioManagerEventChannel.OnMusicFadeLayerIn += FadeMusicLayerIn;
            audioManagerEventChannel.OnMusicFadeLayerOut += FadeMusicLayerOut;

            audioManagerEventChannel.OnSFXPlayRequested += PlayAudioClipsBank;
            audioManagerEventChannel.OnSFXStopRequested += StopAudioClipsBank;
            audioManagerEventChannel.OnSFXFinishRequested += FinishAudioClipsBank;
            audioManagerEventChannel.OnSFXPauseRequested += PauseAudioClipsBank;
            audioManagerEventChannel.OnSFXResumeRequested += ResumeAudioClipsBank;

            audioManagerEventChannel.OnMasterVolumeChanged += MasterVolumeChanged;
            audioManagerEventChannel.OnMusicVolumeChanged += MusicVolumeChanged;
            audioManagerEventChannel.OnSFXVolumeChanged += SFXVolumeChanged;

            audioManagerEventChannel.OnChangeAudioMixerSnapshot += ChangeAudioMixerSnapshot;
        }

        private void OnDisable()
        {
            audioManagerEventChannel.OnMusicPlayRequested -= PlayMusic;
            audioManagerEventChannel.OnMusicStopRequested -= StopMusic;
            audioManagerEventChannel.OnMusicFadeLayerIn += FadeMusicLayerIn;
            audioManagerEventChannel.OnMusicFadeLayerOut += FadeMusicLayerOut;

            audioManagerEventChannel.OnSFXPlayRequested -= PlayAudioClipsBank;
            audioManagerEventChannel.OnSFXStopRequested -= StopAudioClipsBank;
            audioManagerEventChannel.OnSFXFinishRequested -= FinishAudioClipsBank;

            audioManagerEventChannel.OnMasterVolumeChanged -= MasterVolumeChanged;
            audioManagerEventChannel.OnMusicVolumeChanged -= MusicVolumeChanged;
            audioManagerEventChannel.OnSFXVolumeChanged -= SFXVolumeChanged;

            audioManagerEventChannel.OnChangeAudioMixerSnapshot -= ChangeAudioMixerSnapshot;
        }

        private void Awake()
        {
            pooler = GetComponent<Pooler>();

            soundEmitterMap = new SoundEmitterMap();
            musicMap = new SoundEmitterMap();

            // Force audio mixer snapshot to default
            ChangeAudioMixerSnapshot("Default");
        }

        private SoundEmitterKey PlayMusic(SoundBankSO musicClipsBank, AudioConfigurationSO audioConfiguration, float fadeTime, float initialVolume)
        {
            if (musicMap.Contains(musicKey) && musicMap.IsPlaying(musicKey))
            {
                if (musicMap.Get(musicKey, out SoundEmitter[] emitters))
                {
                    if (emitters[0].GetClip() == musicClipsBank.GetClips()[0])
                    {
                        return SoundEmitterKey.Invalid;
                    }
                }

                FadeMusicOut(musicKey, fadeTime);
            }

            AudioClip[] audioClips = musicClipsBank.GetClips();
            SoundEmitter[] emitterRefs = new SoundEmitter[audioClips.Length];
            for (int i = 0; i < audioClips.Length; i++)
            {
                emitterRefs[i] = pooler.GetPooledObject().GetComponent<SoundEmitter>();
                emitterRefs[i].Play(audioClips[i], audioConfiguration);
                emitterRefs[i].GetAudioSource().volume = initialVolume;
                emitterRefs[i].OnSoundFinishedPlaying += OnSoundFinishedPlaying;
            }

            musicKey = musicMap.Add(musicClipsBank, emitterRefs);
            return musicKey;
        }

        private bool StopMusic(SoundEmitterKey key)
        {
            bool isFound = musicMap.Get(key, out SoundEmitter[] emitters);
            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    emitters[i].Stop();
                }

                musicMap.Remove(key);
            }

            return isFound;
        }

        private void FadeMusicOut(SoundEmitterKey key, float fadeTime)
        {
            bool isFound = musicMap.Get(key, out SoundEmitter[] emitters);
            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    FadeMusicLayerOut(key, i, fadeTime);
                }

                musicMap.Remove(key);
            }
        }

        private void FadeMusicLayerIn(SoundEmitterKey key, int layer, float fadeTime)
        {
            bool isFound = musicMap.Get(key, out SoundEmitter[] emitters);
            if (isFound)
            {
                if (layer >= emitters.Length)
                {
                    Debug.LogWarning($"Unable to enable music layer {layer}: index out of bounds.");
                    return;
                }

                var emitter = emitters[layer];
                StartCoroutine(FadeMusicLayerInCoroutine(emitter, fadeTime));
            }
        }

        private IEnumerator FadeMusicLayerInCoroutine(SoundEmitter emitter, float fadeTime)
        {
            float time = 0f;
            var audioSource = emitter.GetAudioSource();
            float startVolume = audioSource.volume;
            float duration = fadeTime;

            while (time < duration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 1f, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = 1f;
        }

        private void FadeMusicLayerOut(SoundEmitterKey key, int layer, float fadeTime)
        {
            bool isFound = musicMap.Get(key, out SoundEmitter[] emitters);
            if (isFound)
            {
                if (layer >= emitters.Length)
                {
                    Debug.LogWarning($"Unable to enable music layer {layer}: index out of bounds.");
                    return;
                }

                var emitter = emitters[layer];
                StartCoroutine(FadeMusicLayerOutCoroutine(emitter, fadeTime));
            }
        }

        private IEnumerator FadeMusicLayerOutCoroutine(SoundEmitter emitter, float fadeTime)
        {
            float time = 0f;
            var audioSource = emitter.GetAudioSource();
            float startVolume = audioSource.volume;
            float duration = fadeTime;

            while (time < duration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = 0f;
            OnSoundFinishedPlaying(emitter);
        }

        private SoundEmitterKey PlayAudioClipsBank(SoundBankSO audioClipsBank, AudioConfigurationSO audioConfiguration, Vector3 position = default)
        {
            AudioClip[] clips = audioClipsBank.GetClips();
            SoundEmitter[] emitterRefs = new SoundEmitter[clips.Length];

            for (int i = 0; i < clips.Length; i++)
            {
                emitterRefs[i] = pooler.GetPooledObject().GetComponent<SoundEmitter>();
                emitterRefs[i].Play(clips[i], audioConfiguration, position: position);
                if (!audioConfiguration.loop)
                {
                    emitterRefs[i].OnSoundFinishedPlaying += OnSoundFinishedPlaying;
                }
            }

            return soundEmitterMap.Add(audioClipsBank, emitterRefs);
        }

        public bool StopAudioClipsBank(SoundEmitterKey key)
        {
            bool isFound = soundEmitterMap.Get(key, out SoundEmitter[] emitters);
            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    OnSoundFinishedPlaying(emitters[i]);
                }

                soundEmitterMap.Remove(key);
            }
            else
            {
                Debug.LogWarning("Stopping AudioClipsBank was requested, but the AudioClipsBank was not found.");
            }

            return isFound;
        }

        public bool FinishAudioClipsBank(SoundEmitterKey key)
        {
            bool isFound = soundEmitterMap.Get(key, out SoundEmitter[] emitters);

            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    emitters[i].Finish();
                    emitters[i].OnSoundFinishedPlaying += OnSoundFinishedPlaying;
                }
            }
            else
            {
                Debug.LogWarning("Finishing AudioClipsBank was requested, but the AudioClipsBank was not found.");
            }

            return isFound;
        }

        public void PauseAudioClipsBank(SoundEmitterKey key)
        {
            bool isFound = soundEmitterMap.Get(key, out SoundEmitter[] emitters);

            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    emitters[i].Pause();
                }
            }
            else
            {
                Debug.LogWarning("Pausing AudioClipsBank was requested, but the AudioClipsBank was not found.");
            }
        }

        public void ResumeAudioClipsBank(SoundEmitterKey key)
        {
            bool isFound = soundEmitterMap.Get(key, out SoundEmitter[] emitters);

            if (isFound)
            {
                for (int i = 0; i < emitters.Length; i++)
                {
                    emitters[i].Resume();
                }
            }
            else
            {
                Debug.LogWarning("Pausing AudioClipsBank was requested, but the AudioClipsBank was not found.");
            }
        }

        private void OnSoundFinishedPlaying(SoundEmitter soundEmitter)
        {
            if (!soundEmitter.Looping())
            {
                soundEmitter.OnSoundFinishedPlaying -= OnSoundFinishedPlaying;
            }

            soundEmitter.Stop();
            soundEmitter.gameObject.SetActive(false);
        }

        private void MasterVolumeChanged(float volume)
        {
            masterVolume = volume;
            audioMixer.SetFloat("Master", NormalizedToMixerValue(volume));
        }

        private void MusicVolumeChanged(float volume)
        {
            musicVolume = volume;
            audioMixer.SetFloat("Music", NormalizedToMixerValue(volume));
        }

        private void SFXVolumeChanged(float volume)
        {
            sfxVolume = volume;
            audioMixer.SetFloat("SFX", NormalizedToMixerValue(volume));
        }

        private float MixerValueToNormalized(float mixerValue)
        {
            // We're assuming the range [-80dB to 0dB] becomes [0 to 1]
            return 1f + (mixerValue / 80f);
        }

        private float NormalizedToMixerValue(float normalizedValue)
        {
            // We're assuming the range [0 to 1] becomes [-80dB to 0dB]
            // This doesn't allow values over 0dB
            return (normalizedValue - 1f) * 80f;
        }

        private void ChangeAudioMixerSnapshot(string snapshotName) =>
            audioMixerSnapshots.FirstOrDefault((snapshot) => snapshot.name == snapshotName).TransitionTo(0f);
    }
}
