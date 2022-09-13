using System;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Models
{
    public class Topic
    {
        [JsonPropertyName("active")]
        public DateTime Active { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("followers")]
        public int Followers { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
