﻿using clan_system.Models.Services;
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
    }
}
