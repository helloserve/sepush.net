using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Models
{
    public class Status
    {
        [JsonPropertyName("capetown")]
        public StatusProvider CapeTown { get; set; }

        [JsonPropertyName("eskom")]
        public StatusProvider Eskom { get; set; }
    }

    public class StatusProvider
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("next_stages")]
        public List<NextStages> NextStages { get; set; }

        [JsonPropertyName("stage")]
        public string Stage { get; set; }

        [JsonPropertyName("stage_updates")]
        public DateTime StageUpdated { get; set; }
    }

    public class NextStages
    {
        [JsonPropertyName("stage")]
        public string Stage { get; set; }

        [JsonPropertyName("stage_start_timestamp")]
        public DateTime StageStartTimestamp { get; set; }
    }
}
