using Microsoft.AspNetCore.Mvc;

namespace web_app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
