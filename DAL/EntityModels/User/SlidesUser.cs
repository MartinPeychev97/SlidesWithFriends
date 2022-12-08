using DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels.User
{
    public class SlidesUser : IdentityUser
    {
        [Required]
        public Subscription Subscription { get; set; } = Subscription.None;
    }
}
