using BAL.Interfaces;
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
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var slide = await this.slideService.GetById(id);
            var slideViewModel = new SlideViewModel 
            {
                Id= id,
                Title = slide.Title,
                Text= slide.Text,
            };

           
            return PartialView("_SlideEdit", slideViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Add(int presentationId)
        {
            var id = await this.slideService.Add(presentationId);

            return new JsonResult(Ok(new {Id = id}));
        }

        [HttpPost]
        public async Task<JsonResult> Remove(int id)
        {
            await this.slideService.Remove(id);

            return new JsonResult(Ok());
        }

        [HttpPost]
        public async Task<JsonResult> Edit(int id, string title, string text)
        {
            await this.slideService.Edit(id, title, text);

            return new JsonResult(Ok());
        }
    }
}
