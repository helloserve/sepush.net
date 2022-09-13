using helloserve.SePush.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Responses
{
    internal class AreaSearchResponse
    {
        [JsonPropertyName("areas")]
        public List<Area> Areas { get; set; }
    }
}
