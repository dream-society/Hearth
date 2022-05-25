using Newtonsoft.Json;
using UnityEngine;

namespace DS.Save
{
    public class Enemy
    {
        public string Type { get; set; }
        public Vector3 Position { get; set; }

        public string ToJson() => JsonConvert.SerializeObject(this);
        public static Enemy FromJson(string json) => JsonConvert.DeserializeObject<Enemy>(json);
    }
}