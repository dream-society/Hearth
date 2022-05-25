using Newtonsoft.Json;

namespace DS.Save
{
    public class Level
    {
        public string Name { get; set; } = default;
        public bool Completed { get; set; } = default;
        // public List<Light> Lights { get; set; } = default;
        // public List<Enemy> Enemies { get; set; }

        public string ToJson() => JsonConvert.SerializeObject(this);
        public static Level FromJson(string json) => JsonConvert.DeserializeObject<Level>(json);
    }
}