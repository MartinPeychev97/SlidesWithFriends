using DAL.Enums;

namespace DAL.EntityModels
{
    public class Slide
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Background { get; set; }
        public int Rating { get; set; }
        public SlideType Type { get; set; }
        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }
    }
}
