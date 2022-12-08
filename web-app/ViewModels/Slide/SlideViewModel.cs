using System.ComponentModel.DataAnnotations;

namespace web_app.ViewModels.Slide
{
    public class SlideViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
