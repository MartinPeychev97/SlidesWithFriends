using Microsoft.AspNetCore.Http;

namespace web_app.ViewModels.Presentation
{
    public class PresentationImageEditViewModel
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; }
    }
}
