using AcFunCard.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcFunCard.Controllers
{
    [Route("/")]
    [ApiController]
    [EnableCors]
    public class CardController : ControllerBase
    {
        private readonly IHttpClientFactory factory;

        public CardController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult> Get(long id, [FromQuery] Config config)
        {
            using var client = factory.CreateClient();

            Club club;
            UserInfo info;

            {
                using var response = await client.GetAsync($"https://live.acfun.cn/rest/pc-direct/fansClub/user/info?userId={id}");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        club = await JsonSerializer.DeserializeAsync<Club>(await response.Content.ReadAsStreamAsync());
                    }
                    catch (JsonException)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            {
                using var response = await client.GetAsync($"https://live.acfun.cn/rest/pc-direct/user/userInfo?userId={id}");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        info = await JsonSerializer.DeserializeAsync<UserInfo>(await response.Content.ReadAsStreamAsync());
                    }
                    catch (JsonException)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            var signatures = info.Profile.Signature
                .Split('\r', '\n')
                .Where(sign => !string.IsNullOrWhiteSpace(sign))
                .ToArray();
            var lineMargin = 0;
            var signature = "";
            foreach (var sign in signatures)
            {
                signature += @$"<tspan x=""0"" y=""{lineMargin}"">{sign}</tspan>";
                lineMargin += 20;
            }
            var height = 155 + lineMargin;
            var detailMargin = 60 + lineMargin;

            var svg = @$"<svg width=""495"" height=""{height}"" viewBox=""0 0 495 {height}"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
    <style>
        .name {{
            font: 600 18px 'Segoe UI', Ubuntu, Sans-Serif;
            fill: #{config.NameColor};
            animation: fadeInAnimation 0.8s ease-in-out forwards;
        }}
        .signature {{
            font: 400 14px 'Segoe UI', Ubuntu, Sans-Serif;
            fill: #{config.SignatureColor};
            animation: fadeInAnimation 0.8s ease-in-out forwards;
        }}
        .stat {{
            font: 600 14px 'Segoe UI', Ubuntu, ""Helvetica Neue"", Sans-Serif;
            fill: #333;
        }}
        .followed {{
            fill: #{config.FollowedColor};
        }}
        .following {{
            fill: #{config.FollowingColor};
        }}
        .content {{
            fill: #{config.ContentColor};
        }}
        .club {{
            fill: #{config.ClubColor};
        }}
        .medal {{
            fill: #{config.MedalColor};
        }}
        .level {{
            fill: #{config.LevelColor};
        }}
        .stagger {{
            opacity: 0;
            animation: fadeInAnimation 0.3s ease-in-out forwards;
        }}

        .bold {{
            font-weight: 700;
        }}
        @keyframes fadeInAnimation {{
            from {{
                opacity: 0;
            }}
            to {{
                opacity: 1;
            }}
        }}
    </style>
    <rect x=""0.5"" y=""0.5"" rx=""4.5"" height=""99%"" stroke=""#{config.BorderColor}"" width=""494"" fill=""#{config.BackgroundColor}"" stroke-opacity=""1"" />
    <g transform=""translate(25, 35)"">
        <g transform=""translate(0, 0)"">
            <text x=""0"" y=""0"" class=""name"">{info.Profile.Name}{config.Caption}</text>
        </g>
    </g>
    <g transform=""translate(25, 60)"">
        <g transform=""translate(0,0)"">
            <text x=""0"" y=""0"" class=""signature"">{signature}</text>
        </g>
    </g>
    <g transform=""translate(0, {detailMargin})"">
        <svg x=""0"" y=""0"">
            <g transform=""translate(0, 0)"">
                <g class=""stagger"" style=""animation-delay: 450ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">粉丝数：</text>
                    <text class=""stat followed"" x=""170"" y=""12.5"">{info.Profile.Followed}</text>
                </g>
            </g>
            <g transform=""translate(248, 0)"">
                <g class=""stagger"" style=""animation-delay: 600ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">关注数：</text>
                    <text class=""stat following"" x=""170"" y=""12.5"">{info.Profile.Following}</text>
                </g>
            </g>
            <g transform=""translate(0, 30)"">
                <g class=""stagger"" style=""animation-delay: 750ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">投稿数：</text>
                    <text class=""stat content"" x=""170"" y=""12.5"">{info.Profile.ContentCount}</text>
                </g>
            </g>
            <g transform=""translate(248, 30)"">
                <g class=""stagger"" style=""animation-delay: 900ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">守护徽章数：</text>
                    <text class=""stat medal"" x=""170"" y=""12.5"">{club.MedalCount}</text>
                </g>
            </g>
            <g transform=""translate(0, 60)"">
                <g class=""stagger"" style=""animation-delay: 1050ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">当前佩戴徽章：</text>
                    <text class=""stat club"" x=""170"" y=""12.5"">{club.WearMedalInfo.ClubName ?? "无"}</text>
                </g>
            </g>
            <g transform=""translate(248, 60)"">
                <g class=""stagger"" style=""animation-delay: 1200ms"" transform=""translate(25, 0)"">
                    <text class=""stat bold"" y=""12.5"">当前佩戴徽章等级：</text>
                    <text class=""stat level"" x=""170"" y=""12.5"">{club.WearMedalInfo.Level}</text>
                </g>
            </g>
        </svg>
    </g>
</svg>";
            return Content(svg, "image/svg+xml", Encoding.UTF8);
        }
    }
}
