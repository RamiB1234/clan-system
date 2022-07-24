using clan_system.Models.Entities;
using clan_system.Models.Services;
using clan_system.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace clan_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        private readonly ClanService clanService;

        public HomeController(UserService userService, ClanService clanService)
        {
            this.userService = userService;
            this.clanService = clanService;
        }

        public IActionResult Index()
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            if (sessionUserName == null)
            {
                return RedirectToAction("Login");
            }

            var clanList= clanService.GetClanList();

            // Checks if user is part of any clan:
            var inClan = false;
            Clan userClan = null;
            foreach(var clan in clanList)
            {
                if(clan.Users != null && clan.Users.Count>0 && inClan==false)
                {
                    foreach(var user in clan.Users)
                    {
                        if(user.UserName == sessionUserName && user.InClan)
                        {
                            inClan = true;
                            userClan = clan;
                            break;
                        }
                    }
                }
            }

            var model = new LandingPageViewModel()
            {
                LoggedUserName = sessionUserName,
                ClanList = clanList,
                UserInClan = inClan,
                clan = userClan

            };

            return View(model);
        }

        public IActionResult Contribution(string clanName)
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            var clanUsers = clanService.GetAllClanUsers(clanName);
            var model = new ContributionViewModel()
            {
                LoggedUserName = sessionUserName,
                ClanName = clanName,
                ClanUsers = clanUsers
            };

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            // For simplicity, we'll use the current timestamp as the unique sessionId for the user:
            var sessionId = DateTime.Now.ToString();

            userService.LoginUser(user.UserName, sessionId);

            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("SessionId", sessionId);

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("SessionId");
            return RedirectToAction("Index");
        }

        [HttpGet("/Home/CheckUserSession")]
        public IActionResult CheckUserSession()
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            var sessionId = HttpContext.Session.GetString("SessionId");

            var userHasAnotherSession = userService.UserHasAnotherSession(sessionUserName, sessionId);
            return Ok(userHasAnotherSession);

        }

    }
}
