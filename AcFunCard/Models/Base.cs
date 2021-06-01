using System.Text.Json.Serialization;

namespace AcFunCard.Models
{
    public record Base
    {
        [JsonPropertyName("result")]
        public int Result { get; init; }
        [JsonPropertyName("host-name")]
        public string HostName { get; init; }
    }
}
