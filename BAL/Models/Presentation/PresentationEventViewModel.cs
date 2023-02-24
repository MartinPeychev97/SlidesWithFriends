using BAL.Models.Slide;
using System.Collections.Generic;

namespace BAL.Models.Presentation
{
    public class PresentationEventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public IEnumerable<SlideEventViewModel> Slides { get; set; }
    }
}
