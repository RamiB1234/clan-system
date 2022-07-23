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

            var model = new LandingPageViewModel()
            {
                LoggedUserName = sessionUserName
            };

            // Should return either _LandingWithClan or _LandingWithoutClan
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
