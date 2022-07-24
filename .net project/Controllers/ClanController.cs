using clan_system.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clan_system.Controllers
{
    public class ClanController : Controller
    {
        private readonly ClanService clanService;
        public ClanController(ClanService clanService)
        {
            this.clanService = clanService;
        }

        [HttpPost("/Clan/JoinClan")]
        public IActionResult JoinClan(string clanName)
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            clanService.JoinClan(sessionUserName, clanName);
            return Ok();

        }

        [HttpPost("/Clan/LeaveClan")]
        public IActionResult LeaveClan(string clanName)
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            clanService.LeaveClan(sessionUserName, clanName);
            return Ok();

        }

        [HttpPost("/Clan/AddPoints")]
        public IActionResult AddPoints(string clanName, int points)
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");

            clanService.AddPoints(points, sessionUserName, clanName);
            return Ok();

        }
    }
}
