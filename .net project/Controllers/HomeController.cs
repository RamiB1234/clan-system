using clan_system.Models.Entities;
using clan_system.Models.Services;
using clan_system.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            foreach(var clan in clanList)
            {
                if(clan.Users != null && clan.Users.Count>0 && inClan==false)
                {
                    foreach(var user in clan.Users)
                    {
                        if(user.UserName == sessionUserName)
                        {
                            inClan = true;
                            break;
                        }
                    }
                }
            }

            var model = new LandingPageViewModel()
            {
                LoggedUserName = sessionUserName,
                ClanList = clanList,
                UserInClan = inClan
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
            userService.LoginUser(user.UserName);

            HttpContext.Session.SetString("UserName", user.UserName);

            return RedirectToAction("Index");
        }

        public IActionResult Contribution()
        {
            return View();
        }

    }
}
