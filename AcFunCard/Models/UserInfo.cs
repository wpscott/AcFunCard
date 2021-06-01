using System.Text.Json.Serialization;

namespace AcFunCard.Models
{
    public sealed record UserInfo : Base
    {
        [JsonPropertyName("profile")]
        public Profile Profile { get; init; }
    }

    public sealed record Profile
    {
        [JsonPropertyName("contentCount")]
        public string ContentCount { get; init; }
        [JsonPropertyName("followed")]
        public string Followed { get; init; }
        [JsonPropertyName("following")]
        public string Following { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; }
        [JsonPropertyName("signature")]
        public string Signature { get; init; }
    }
}
