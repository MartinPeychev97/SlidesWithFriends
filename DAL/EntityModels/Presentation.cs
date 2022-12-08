using System.Collections.Generic;

namespace DAL.EntityModels
{
    public class Presentation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Slide> Slides { get; set; } = new List<Slide>();
    }
}
