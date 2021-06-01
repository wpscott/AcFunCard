using System.Text.Json.Serialization;

namespace AcFunCard.Models
{
    public sealed record Club : Base
    {
        [JsonPropertyName("medalCount")]
        public int MedalCount { get; init; }
        [JsonPropertyName("wearMedalInfo")]
        public MedalInfo WearMedalInfo { get; init; }
    }

    public sealed record MedalInfo
    {
        [JsonPropertyName("clubName")]
        public string ClubName { get; init; }
        [JsonPropertyName("level")]
        public int Level { get; init; }
        [JsonPropertyName("uperId")]
        public long UperId { get; init; }
        [JsonPropertyName("uperName")]
        public string UperName { get; init; }
        [JsonPropertyName("uperHeadUrl")]
        public string UperHeadUrl { get; init; }
    }
}
