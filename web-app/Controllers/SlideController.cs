using BAL.Services;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
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

        [HttpPost]
        public async Task<IActionResult> Add(string title, string text, int presentationId)
        {
            await this.slideService.Add(title, text, presentationId);


            return RedirectToAction("Edit", "Presentation", new { id = presentationId });
        }

        [HttpGet]
        public async Task<IActionResult> SlideEditPartial(int slideId, int presentationId)
        {
            var slide = await this.slideService.GetById(slideId);

            if(slideId == 0)
            {
                return PartialView("_SlideEdit", new Slide { Title = "Title", Text = "Text", PresentationId = presentationId});
            }

            return PartialView("_SlideEdit", slide);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int slideId, int presentationId)
        {
            await this.slideService.Remove(slideId);

            return RedirectToAction("Edit", "Presentation", new { id = presentationId });
        }
    }
}
