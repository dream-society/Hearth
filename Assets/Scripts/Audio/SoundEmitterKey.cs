namespace DS.Audio
{

    public class SoundEmitterKey
    {
        public static SoundEmitterKey Invalid = new SoundEmitterKey(-1, null);

        internal int Value;
        internal SoundBankSO SoundBank;

        internal SoundEmitterKey(int value, SoundBankSO soundBank)
        {
            Value = value;
            SoundBank = soundBank;
        }

        public override bool Equals(object obj) =>
            obj is SoundEmitterKey other &&
            Value == other.Value &&
            SoundBank == other.SoundBank;

        public override int GetHashCode()
        {
            return (Value, SoundBank).GetHashCode();
        }

        public static bool operator ==(SoundEmitterKey thiz, SoundEmitterKey other) =>
            thiz.GetHashCode() == other.GetHashCode() &&
            thiz.Value == other.Value &&
            thiz.SoundBank == other.SoundBank;

        public static bool operator !=(SoundEmitterKey thiz, SoundEmitterKey other) => !(thiz == other);
    }
}
