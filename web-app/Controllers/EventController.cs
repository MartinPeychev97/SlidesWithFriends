using BAL.Interfaces;
using BAL.Models.Event;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace web_app.Controllers
{
    public class EventController : Controller
    {
        private readonly IUsernameGenerator _usernameGenerator;

        public EventController(IUsernameGenerator usernameGenerator)
        {
            this._usernameGenerator = usernameGenerator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Presentation(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var username = await this._usernameGenerator.GenerateUsername(id, userId);

                var model = new EventStartViewModel
                {
                    Username = username.AsCountry.ToUpper(),
                    QRCode = null
                };

                return View(model);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
