using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using web_app.ViewModels.Slide;

namespace web_app.ViewModels.Presentation
{
    public class PresentationViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public SlideViewModel ActiveSlide { get; set; }

        public List<SlideViewModel> Slides { get; set; }
    }
}
