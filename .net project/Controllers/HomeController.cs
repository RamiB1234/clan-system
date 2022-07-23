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

        public HomeController(UserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");
            if (sessionUserName == null)
            {
                return RedirectToAction("Login");
            }

            /* To-Do:
             * -Retrieve list of clans
             * -Check if user is part of any clan
             * -if not, display _LandingWithoutClan. Otherwise display _LandingWithClan 
             */

            var model = new LandingPageViewModel()
            {
                LoggedUserName = sessionUserName
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
