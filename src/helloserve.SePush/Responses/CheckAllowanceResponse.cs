using helloserve.SePush.Models;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Responses
{
    internal class CheckAllowanceResponse
    {
        [JsonPropertyName("allowance")]
        public Allowance Allowance { get; set; }
    }
}
