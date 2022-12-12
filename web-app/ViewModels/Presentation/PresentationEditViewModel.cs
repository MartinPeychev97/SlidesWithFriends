using System.Collections.Generic;
using web_app.ViewModels.Slide;

namespace web_app.ViewModels.Presentation
{
    public class PresentationEditViewModel
    {
        public string Name { get; set; }

        public IEnumerable<SlideViewModel> Slides { get; set; }
    }
}
