using BAL.Interfaces;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SlideEditPartial(int slideId, int presentationId)
        {
            var slide = await this.slideService.GetById(slideId);

            if (slideId == 0)
            {
                return PartialView("_SlideEdit", new Slide
                {
                    Title = "Title",
                    Text = "Text",
                    PresentationId = presentationId
                });
            }

            return PartialView("_SlideEdit", slide);
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
