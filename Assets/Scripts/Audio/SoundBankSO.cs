using System;
using UnityEngine;

namespace DS.Audio
{
    [CreateAssetMenu(fileName = "SoundBank", menuName = "DreamSociety/Audio/Sound Bank")]
    public class SoundBankSO : ScriptableObject
    {
        public SoundGroup[] soundGroups = default;

        public AudioClip[] GetClips()
        {
            AudioClip[] clips = new AudioClip[soundGroups.Length];
            for (int i = 0; i < soundGroups.Length; i++)
            {
                clips[i] = soundGroups[i].NextSound();
            }

            return clips;
        }
    }

    [Serializable]
    public class SoundGroup
    {
        public enum AudioSequenceMode
        {
            Sequential,
            Random,
        }

        public AudioSequenceMode sequenceMode = AudioSequenceMode.Random;
        public AudioClip[] audioClips;

        private int currentIndex = -1;
        private int previousIndex = -1;

        public AudioClip NextSound()
        {
            // Returns the only clip to play
            if (audioClips.Length == 1) return audioClips[0];

            switch (sequenceMode)
            {
                case AudioSequenceMode.Sequential:
                    currentIndex = (int)Mathf.Repeat(++currentIndex, audioClips.Length);
                    break;
                case AudioSequenceMode.Random:
                    do
                    {
                        currentIndex = UnityEngine.Random.Range(0, audioClips.Length);
                    }
                    while (currentIndex == previousIndex); // Avoid repetitions
                    break;
            }

            previousIndex = currentIndex;

            return audioClips[currentIndex];
        }
    }
}
