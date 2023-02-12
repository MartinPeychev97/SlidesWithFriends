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
using web_app.ViewModels.Presentation;
using BAL.Models;

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
        public async Task<IActionResult> Presentation(int id, bool isPresenter)
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
                        Rating = s.Rating,
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
    }
}
