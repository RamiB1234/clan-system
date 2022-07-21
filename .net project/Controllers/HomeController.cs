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
            return View();
        }

        public IActionResult Login()
        {
            // Should return either _LandingWithClan or _LandingWithoutClan
            return View();
        }

        public IActionResult Contributuin()
        {
            return View();
        }

    }
}
