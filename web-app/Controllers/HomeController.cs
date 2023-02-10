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
        private readonly IPresentationService presentationService;

        public HomeController(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var presentation = await this.presentationService.GetAll(userId);

            return View(presentation);
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
