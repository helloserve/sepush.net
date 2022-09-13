using System.Text.Json.Serialization;

namespace helloserve.SePush.Models
{
    public class AreaNearby
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Area
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
    }
}
