using BAL.Interfaces;
using BAL.Models.Slide;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using web_app.ViewModels.Slide;

namespace web_app.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideService slideService;
        private readonly IWebHostEnvironment hostEnvironment;

        public SlideController(ISlideService slideService, IWebHostEnvironment hostEnvironment)
        {
            this.slideService = slideService;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<JsonResult> AddTitleSlide([FromQuery] int presentationId)
        {
            var slide = await this.slideService.AddTitleSlide(presentationId);

            if (slide is null)
            {
                return new JsonResult(NotFound());
            }

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Title = slide.Title,
                Text = slide.Text,
                Type = slide.Type.ToString(),
            };

            return new JsonResult(slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddImageSlide([FromForm] SlideImageViewModel viewModel)
        {
            if (viewModel.Image == null && viewModel.Image.ContentType.StartsWith("image"))
            {
                return new JsonResult(BadRequest());
            }

            string wwwroot = hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwroot, @"images", @"slides");
            var extension = Path.GetExtension(viewModel.Image.FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                viewModel.Image.CopyTo(fileStream);
            }

            var imagePath = @"\images\slides\" + fileName + extension;

            var slide = await this.slideService.AddImageSlide(viewModel.PresentationId, imagePath);

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Image = slide.Image,
                Text = slide.Text,
                Type = slide.Type.ToString(),
            };

            return new JsonResult(slideViewModel);
        }

        [HttpPut]
        public async Task<JsonResult> EditTitle([FromBody] SlideTitleEditViewModel viewModel)
        {
            var result = await this.slideService.EditTitle(viewModel.Id, viewModel.Title);

            if (result is false)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok());
        }

        [HttpPut]
        public async Task<JsonResult> EditText([FromBody] SlideTextEditViewModel viewModel)
        {
            var result = await this.slideService.EditText(viewModel.Id, viewModel.Text);

            if (result is false)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok());
        }

        [HttpPut]
        public async Task<JsonResult> EditOnDragAndDrop([FromBody] SlideDragAndDropViewModel viewModel)
        {
            var result = await this.slideService.EditOnDragAndDrop(viewModel.FirstId, viewModel.SecondId);

            if (result is false)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok());
        }

        [HttpPut]
        public async Task<JsonResult> EditBackground([FromBody] EditSlideBackgroundInputModel model)
        {
            try
            {
                await this.slideService.EditBackground(model);

                return new JsonResult(Ok(model.Background));
            }
            catch (Exception)
            {
                return new JsonResult(NotFound());
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Remove([FromQuery] int id)
        {
            var slide = await this.slideService.GetById(id);

            if (slide is null)
            {
                return new JsonResult(NotFound());
            }

            if (slide.Image != null)
            {
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, slide.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await this.slideService.Remove(id);

            return new JsonResult(Ok());
        }

        [HttpPost]
        public async Task<JsonResult> AddRatingSlide([FromBody] SlideRatingViewModel viewModel)
        {
            var slide = await this.slideService.AddRatingSlide(viewModel.PresentationId, viewModel.Rating);

            if (slide is null)
            {
                return new JsonResult(NotFound());
            }

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Rating = slide.Rating,
                Type = slide.Type.ToString(),
            };

            return new JsonResult(slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddWordCloudSlide([FromQuery] int presentationId)
        {
            var slide = await this.slideService.AddWordCloudSlide(presentationId);

            if (slide is null)
            {
                return new JsonResult(NotFound());
            }

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Title = slide.Title,
                Text = slide.Text,
                Type = slide.Type.ToString(),
            };

            return new JsonResult(slideViewModel);
        }

    }
}
