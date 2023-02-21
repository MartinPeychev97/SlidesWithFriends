using Microsoft.AspNetCore.Http;

namespace web_app.ViewModels.Slide
{
    public class SlideImageViewModel
    {
        public IFormFile Image { get; set; }

        public int PresentationId { get; set; }
    }
}
