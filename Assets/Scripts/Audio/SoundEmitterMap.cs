using System.Collections.Generic;

namespace DS.Audio
{

    public class SoundEmitterMap
    {
        private int nextKey = 0;
        private List<SoundEmitterKey> keys;
        public List<SoundEmitter[]> emittersList;

        public SoundEmitterMap()
        {
            keys = new List<SoundEmitterKey>();
            emittersList = new List<SoundEmitter[]>();
        }

        private SoundEmitterKey GetUniqueKey(SoundBankSO audioClipsBank)
        {
            return new SoundEmitterKey(nextKey++, audioClipsBank);
        }

        public void Add(SoundEmitterKey key, SoundEmitter[] emitters)
        {
            keys.Add(key);
            emittersList.Add(emitters);
        }

        public SoundEmitterKey Add(SoundBankSO audioClipsBank, SoundEmitter[] emitters)
        {
            var key = GetUniqueKey(audioClipsBank);
            Add(key, emitters);
            return key;
        }

        public bool Get(SoundEmitterKey key, out SoundEmitter[] emitters)
        {
            int index = keys.FindIndex(v => v == key);

            if (index < 0)
            {
                emitters = null;
                return false;
            }

            emitters = emittersList[index];
            return true;
        }

        public bool Remove(SoundEmitterKey key)
        {
            int index = keys.FindIndex(v => v == key);

            if (index < 0)
            {
                return false;
            }

            keys.RemoveAt(index);
            emittersList.RemoveAt(index);

            return true;
        }

        public bool Contains(SoundEmitterKey key)
        {
            int index = keys.FindIndex(v => v == key);
            if (index < 0)
            {
                return false;
            }

            return true;
        }

        public bool IsPlaying(SoundEmitterKey key)
        {
            int index = keys.FindIndex(v => v == key);
            if (index < 0)
            {
                return false;
            }

            var emitters = emittersList[index];
            for (int i = 0; i < emitters.Length; i++)
            {
                if (!emitters[i].IsPlaying())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
