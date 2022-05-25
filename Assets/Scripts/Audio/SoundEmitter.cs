using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DS.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public AudioSource audioSource { get; private set; }
        public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip, AudioConfigurationSO audioConfiguration, Vector3 position = default)
        {
            audioSource.clip = clip;
            audioConfiguration.ApplyTo(audioSource);

            audioSource.Play();

            if (!audioConfiguration.loop)
            {
                StartCoroutine(AudioClipFinishPlaying(clip.length));
            }
        }

        public void Resume() => audioSource.UnPause();
        public void Pause() => audioSource.Pause();
        public void Stop() => audioSource.Stop();

        /// <summary>
        /// Finish playing audio clip and stop looping.
        /// </summary>
        public void Finish()
        {
            if (!audioSource.loop) return;

            audioSource.loop = false;
            float clipLengthRemaining = audioSource.clip.length - audioSource.time;
            StartCoroutine(AudioClipFinishPlaying(clipLengthRemaining));
        }

        // public void FadeMusicIn(AudioClip music, AudioConfigurationSO audioConfiguration)
        // {
        //     StartCoroutine(FadeIn(music, audioConfiguration));
        // }

        // private IEnumerator FadeIn(AudioClip music, AudioConfigurationSO audioConfiguration)
        // {
        //     if (!IsPlaying())
        //     {
        //         Play(music, audioConfiguration);
        //         audioSource.volume = 0f;
        //     }

        //     while (audioSource.volume < audioConfiguration.volume)
        //     {
        //         float time = 0f;
        //         float startVolume = audioSource.volume;

        //         while (time < audioConfiguration.fadeTime)
        //         {
        //             audioSource.volume = Mathf.Lerp(startVolume, audioConfiguration.volume, time / audioConfiguration.fadeTime);
        //             time += Time.deltaTime;
        //             yield return null;
        //         }

        //         audioSource.volume = audioConfiguration.volume;
        //     }

        //     yield break;
        // }

        // public void FadeMusicOut(AudioConfigurationSO audioConfiguration, float fadeTime)
        // {
        //     StartCoroutine(AudioClipFinishPlaying(fadeTime));
        //     StartCoroutine(FadeOut(audioConfiguration));
        // }

        // private IEnumerator FadeOut(AudioConfigurationSO newMusicConfiguration)
        // {
        //     while (audioSource.volume > 0f)
        //     {
        //         float time = 0f;
        //         float startVolume = audioSource.volume;

        //         while (time < newMusicConfiguration.fadeTime)
        //         {
        //             audioSource.volume = Mathf.Lerp(startVolume, 0, time / newMusicConfiguration.fadeTime);
        //             time += Time.deltaTime;
        //             yield return null;
        //         }
        //     }

        //     audioSource.volume = 0f;
        //     audioSource.Stop();
        // }

        public bool IsPlaying() => audioSource.isPlaying;

        public bool Looping() => audioSource.loop;

        private IEnumerator AudioClipFinishPlaying(float length)
        {
            yield return new WaitForSeconds(length);
            OnSoundFinishedPlaying?.Invoke(this);
        }

        public AudioSource GetAudioSource() => audioSource;
        public AudioClip GetClip() => audioSource.clip;

    }
}
