using BAL.Interfaces;
using BAL.Models.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
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
                    QRCode = GenerateQRCode()
                };

                return View(model);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private string GenerateQRCode()
        {
            var callingUrl = Request.GetTypedHeaders().Referer.ToString();
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
