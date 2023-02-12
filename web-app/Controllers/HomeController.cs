using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

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
    }
}
