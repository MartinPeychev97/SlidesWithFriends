using BAL.Interfaces;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SlideEditPartial(int slideId)
        {
            if (slideId == 0)
            {
                return PartialView("_SlideEdit", new SlideViewModel
                {
                    Title = "Title",
                    Text = "Text",
                });
            }

            var slide = await this.slideService.GetById(slideId);
            var slideViewModel = new SlideViewModel 
            {
                Id= slideId,
                Title = slide.Title,
                Text= slide.Text,
                PresentationBackground = slide.Presentation.Background
            };

           
            return PartialView("_SlideEdit", slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Add(string title, string text, int presentationId)
        {
            await this.slideService.Add(title, text, presentationId);

            return new JsonResult(Ok());
        }

        [HttpPost]
        public async Task<JsonResult> Remove(int id)
        {
            await this.slideService.Remove(id);

            return new JsonResult(Ok());
        }
    }
}
