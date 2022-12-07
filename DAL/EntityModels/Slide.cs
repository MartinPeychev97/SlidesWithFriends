using System;

namespace DAL.EntityModels
{
    public class Slide
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }
    }
}
