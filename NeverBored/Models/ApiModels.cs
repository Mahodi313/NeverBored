using Newtonsoft.Json;

namespace NeverBored.Models
{
    public class Root
    {
        [JsonProperty("activity")]
        public string Activity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("participants")]
        public int? Participants { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
