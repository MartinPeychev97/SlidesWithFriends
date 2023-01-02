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
        public ActionResult Index()
        {
            return View();
        }

        public string GenerateQRCode()
        {
            var callingUrl = Request.GetTypedHeaders().Referer.ToString();

            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(callingUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap bitMap = qrCode.GetGraphic(20);
                bitMap.Save(ms, ImageFormat.Png);
                return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
        }

        //public IActionResult GenerateQrCode()
        //{
            
        //    var callingUrl = Request.GetTypedHeaders().Referer.ToString();

        //    using var qrgenerator = new QRCodeGenerator();
        //    var qrCodeData = qrgenerator.CreateQrCode(callingUrl, QRCodeGenerator.ECCLevel.L);
            
        //    var qrCode = new PngByteQRCode(qrCodeData);
        //    var qrCodeImage = qrCode.GetGraphic(20);
        //    var output = Convert.ToBase64String(qrCodeImage);

        //    return View(output);
            
        //}

        //[NonAction]
        //private static Byte[] BitmapToBytesCode(Bitmap image)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}
    }
}
