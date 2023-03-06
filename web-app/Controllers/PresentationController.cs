using BAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web_app.ViewModels.Presentation;
using web_app.ViewModels.Slide;

namespace web_app.Controllers
{
    public class PresentationController : Controller
    {
        private readonly IPresentationService presentationService;
        private readonly IWebHostEnvironment hostEnvironment;

        public PresentationController(IPresentationService presentationService, IWebHostEnvironment hostEnvironment)
        {
            this.presentationService = presentationService;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult PresentationIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresentationCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
               
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string imagePath = @"\images\presentation\default.png";

            await this.presentationService.Create(viewModel.Name, userId, imagePath);

            return RedirectToAction("PresentationIndex" ,"Presentation");
        }


        [HttpGet]
        public  IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetById([FromQuery] int id)
        {
            var presentation = await this.presentationService.GetById(id);
            var slides = presentation.Slides
                .Select(s => new SlideViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Text = s.Text,
                    Image = s.Image,
                    Background = s.Background,
                    Type = s.Type.ToString(),
                    Rating= s.Rating,
                }).ToList();

            var presentationViewModel = new PresentationEditViewModel()
            {
                Id = presentation.Id,
                Name = presentation.Name,
                Image = presentation.Image,
                Slides = slides
            };

            return new JsonResult(presentationViewModel);
        }

        [HttpPut]
        public async Task<JsonResult> EditName([FromBody] PresentationNameViewModel viewModel)
        {
            await this.presentationService.EditName(viewModel.Id, viewModel.Name);

            return new JsonResult(Ok());
        }

        [HttpPut]
        public async Task<JsonResult> EditImage([FromForm] PresentationImageEditViewModel viewModel)
        {
            if (viewModel.Image == null && viewModel.Image.ContentType.StartsWith("image"))
            {
                return new JsonResult(BadRequest());
            }

            string wwwroot = hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwroot, @"images", @"presentation");
            var extension = Path.GetExtension(viewModel.Image.FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                viewModel.Image.CopyTo(fileStream);
            }

            var imagePath = @"\images\presentation\" + fileName + extension;

            await this.presentationService.EditImage(viewModel.Id, imagePath);

            return new JsonResult(Ok(imagePath));
        }

        public async Task<IActionResult> Remove(int id)
        {
            var presentation = await this.presentationService.GetById(id);
            if (presentation is null)
            {
                return new JsonResult(NotFound());
            }

            await this.presentationService.Remove(presentation.Id);

            return RedirectToAction("PresentationIndex", "Presentation");
        }
    }
}
