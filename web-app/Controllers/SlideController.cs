using BAL.Interfaces;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var slide = await this.slideService.GetById(id);
            var slideViewModel = new SlideViewModel 
            {
                Id= id,
                Title = slide.Title,
                Text= slide.Text,
                PresentationBackground = slide.Presentation.Background
            };

           
            return PartialView("_SlideEdit", slideViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll([FromQuery] int presentationId)
        {
            var slides = await this.slideService.GetAll(presentationId);
            var slideViewModel = slides.Select(s => new SlideViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Text = s.Text,
            });

            return new JsonResult(slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Add([FromQuery] int presentationId)
        {
            var id = await this.slideService.Add(presentationId);

            var slideViewModel = new SlideViewModel
            {
                Id = id,
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
