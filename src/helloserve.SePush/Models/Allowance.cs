using System.Text.Json.Serialization;

namespace helloserve.SePush.Models
{
    public class Allowance
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AllowanceType Type { get; set; }
    }

    public enum AllowanceType
    {
        Daily,
        Weekly,
        Monthly
    }
}
