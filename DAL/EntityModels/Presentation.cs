using DAL.EntityModels.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels
{
    public class Presentation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }
        public SlidesUser User { get; set; }
        
        public ICollection<Rating> PresentationRatings {get; set; } = new List<Rating>();

        public ICollection<Slide> Slides { get; set; } = new List<Slide>();
    }
}

