using helloserve.SePush.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Responses
{
    internal class AreaNearbyResponse
    {
        [JsonPropertyName("areas")]
        public List<AreaNearby> Areas { get; set; }
    }
}
