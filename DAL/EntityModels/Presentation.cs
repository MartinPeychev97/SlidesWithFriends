using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels
{
    public class Presentation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Background { get; set; } = null;

        public ICollection<Slide> Slides { get; set; } = new List<Slide>();
    }
}

