using helloserve.SePush.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Responses
{
    internal class NearbyTopicsResponse
    {
        [JsonPropertyName("topics")]
        public List<Topic> Topics { get; set; }
    }
}
