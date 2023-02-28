using BAL.Interfaces;
using BAL.Models.Event;
using BAL.Models.Presentation;
using BAL.Models.Slide;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using BAL.Models;
using Microsoft.AspNetCore.Identity;
using DAL.EntityModels.User;
using BAL.Services;

namespace web_app.Controllers
{
    public class EventController : Controller
    {
        private readonly IUsernameGenerator _usernameGenerator;
        private readonly IPresentationService presentationService;
        private readonly UserManager<SlidesUser> userManager;
        private readonly ISlideService slideService;

        public EventController(
            IUsernameGenerator usernameGenerator, 
            IPresentationService presentationService,
            UserManager<SlidesUser> userManager,
            ISlideService slideservice)
        {
            this._usernameGenerator = usernameGenerator;
            this.presentationService = presentationService;
            this.userManager = userManager;
            this.slideService = slideservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Presentation(int id, bool isPresenter)
        {
            try
            
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await userManager.FindByIdAsync(userId);

                var presentation = await this.presentationService.GetById(id);
                var slides = presentation.Slides
                    .Select(s => new SlideEventViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Text = s.Text,
                        Image = s.Image,
                        Background = s.Background,
                        Rating = s.Rating,
                        Type = s.Type.ToString(),
                        WordCloudAnswers = s.WordSlideAnswers
                    }).ToList();

                var presentationViewModel = new PresentationEventViewModel()
                {
                    Id = presentation.Id,
                    Name = presentation.Name,
                    Image = presentation.Image,
                    Slides = slides
                };

                var model = new EventStartViewModel
                {
                    Username = user.UserName,
                    Image = user.Image,
                    QRCodeViewModel = new QrCodeViewModel()
                    {
                        QRCode = GenerateQRCode(presentationViewModel.Id)
                    },
                    IsPresenter = isPresenter,
                    Presentation = presentationViewModel,
                };

                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction("PresentationIndex", "Presentation");
            }
        }

        private string GenerateQRCode(int presentationId)
        {
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var callingUrl = $"{baseUrl}/event/presentation/{presentationId}";
            MemoryStream ms = new MemoryStream();

            using QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(callingUrl, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                bitMap.Save(ms, ImageFormat.Png);
            }
            string result = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> WordCloudSubmit(string id, string message)
        {
            var idArray = id.Split(" ");
            var presentationId = int.Parse(idArray[0]);
            var slideId = int.Parse(idArray[1]);
            var slideIndex = int.Parse(idArray[2]);

            var slide = await slideService.GetById(slideId);
            slide.WordSlideAnswers.Add(message);

            return RedirectToAction("Presentation", new { id = presentationId });
        }
    }
}
