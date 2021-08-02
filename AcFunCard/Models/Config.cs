using Microsoft.AspNetCore.Mvc;

namespace AcFunCard.Models
{
    public sealed record Config
    {
        [FromQuery(Name = "caption")]
        public string Caption { get; init; } = "在AcFun";
        [FromQuery(Name = "name_color")]
        public string NameColor { get; init; } = "fd4c5d";
        [FromQuery(Name = "sign_color")]
        public string SignatureColor { get; init; } = "acf";
        [FromQuery(Name = "bg_color")]
        public string BackgroundColor { get; init; } = "fefefe";
        [FromQuery(Name = "border_color")]
        public string BorderColor { get; init; } = "ccc";
        [FromQuery(Name = "followed_color")]
        public string FollowedColor { get; init; } = "333";
        [FromQuery(Name = "following_color")]
        public string FollowingColor { get; init; } = "333";
        [FromQuery(Name = "content_color")]
        public string ContentColor { get; init; } = "333";
        [FromQuery(Name = "club_color")]
        public string ClubColor { get; init; } = "333";
        [FromQuery(Name = "medal_color")]
        public string MedalColor { get; init; } = "333";
        [FromQuery(Name = "level_color")]
        public string LevelColor { get; init; } = "333";
    }
}
