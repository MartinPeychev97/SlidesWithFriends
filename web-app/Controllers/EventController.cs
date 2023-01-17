using BAL.Interfaces;
using BAL.Models.Event;
using BAL.Models.Presentation;
using BAL.Models.Slide;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web_app.ViewModels.Presentation;

namespace web_app.Controllers
{
    public class EventController : Controller
    {
        private readonly IUsernameGenerator _usernameGenerator;
        private readonly IPresentationService presentationService;

        public EventController(IUsernameGenerator usernameGenerator, IPresentationService presentationService)
        {
            this._usernameGenerator = usernameGenerator;
            this.presentationService = presentationService;
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

                var presentation = await this.presentationService.GetById(id);
                var slides = presentation.Slides
                    .Select(s => new SlideEventViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Text = s.Text,
                        Image = s.Image,
                        Background = s.Background,
                        Type = s.Type.ToString(),
                    }).ToList();

                var presentationViewModel = new PresentationEventViewModel()
                {
                    Id = presentation.Id,
                    Name = presentation.Name,
                    Slides = slides
                };

                var model = new EventStartViewModel
                {
                    Username = username.AsCountry.ToUpper(),
                    QRCode = null,
                    Presentation = presentationViewModel,
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
