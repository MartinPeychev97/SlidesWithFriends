using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace web_app.Controllers
{
    public class QrCodeController : Controller
    {
        public IActionResult GenerateQRCode()
        {
            var callingUrl = Request.GetTypedHeaders().Referer.ToString();

            using (MemoryStream ms = new MemoryStream())
            {
                using QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(callingUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View();
        }
    }
}
