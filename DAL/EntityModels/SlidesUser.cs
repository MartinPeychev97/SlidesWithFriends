using DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels.User
{
    public class SlidesUser : IdentityUser
    {
        [Required]
        public Subscription Subscription { get; set; } = Subscription.None;

        public string Image { get; set; }
        public ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();
        public ICollection<Rating> PresentationRatings { get; set; } = new List<Rating>();
    }
}