using Microsoft.AspNetCore.Mvc;

namespace clan_system.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            // Should return either _LandingWithClan or _LandingWithoutClan
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contribution()
        {
            return View();
        }

    }
}
