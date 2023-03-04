using System.Collections.Generic;
using web_app.ViewModels.Slide;

namespace web_app.ViewModels.Presentation
{
    public class PresentationEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public IEnumerable<SlideViewModel> Slides { get; set; }
    }
}
