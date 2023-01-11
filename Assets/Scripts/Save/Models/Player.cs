using Newtonsoft.Json;
using UnityEngine;

namespace DS.Save
{
    public class Player
    {
        public string Scene { get; set; } = default;
        public Vector3 Position { get; set; } = default;
        public int Bottles { get; set; } = default;
        public bool PowerUnlocked { get; set; } = default;

        public string ToJson() => JsonConvert.SerializeObject(this);
        public static Player FromJson(string json) => JsonConvert.DeserializeObject<Player>(json);
    }
}

