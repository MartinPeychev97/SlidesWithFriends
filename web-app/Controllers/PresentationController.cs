using BAL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace web_app.Controllers
{
    public class PresentationController : Controller
    {
        private readonly IPresentationService presentationService;

        public PresentationController(IPresentationService presentationService)
        {
            this.presentationService = presentationService;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var presentation = await this.presentationService.GetById(id);

            return View(presentation);
        }
    }
}
