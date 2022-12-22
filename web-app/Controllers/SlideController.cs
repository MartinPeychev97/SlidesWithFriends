using BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using web_app.ViewModels.Presentation;
using web_app.ViewModels.Slide;

namespace web_app.Controllers
{
    public class SlideController : Controller
    {
        private readonly ISlideService slideService;

        public SlideController(ISlideService slideService)
        {
            this.slideService = slideService;
        }

        [HttpPost]
        public async Task<JsonResult> AddTitleSlide([FromQuery] int presentationId)
        {
            var slide = await this.slideService.AddTitleSlide(presentationId);

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Title = slide.Title,
                Text = slide.Text,
                Image = slide.Image,
            };

            return new JsonResult(slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddImageSlide([FromForm] IFormFile image, [FromForm] string json)
        {
            var presentation = JsonConvert.DeserializeObject<PresentationNameViewModel>(json);
            var slide = await this.slideService.AddImageSlide(presentation.Id, image);

            var slideViewModel = new SlideViewModel
            {
                Id = slide.Id,
                Image = slide.Image
            };

            return new JsonResult(slideViewModel);
        }

        [HttpDelete]
        public async Task<JsonResult> Remove([FromQuery] int id)
        {
            await this.slideService.Remove(id);

            return new JsonResult(Ok());
        }

        [HttpPut]
        public async Task<JsonResult> Edit([FromBody] SlideViewModel viewModel)
        {
            await this.slideService.Edit(viewModel.Id, viewModel.Title, viewModel.Text);

            return new JsonResult(Ok());
        }
    }
}
