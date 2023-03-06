using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace web_app.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
