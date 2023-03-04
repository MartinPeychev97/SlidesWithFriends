using DAL.EntityModels;
using DAL.EntityModels.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SlidesDbContext : IdentityDbContext<SlidesUser>
    {
        public SlidesDbContext(DbContextOptions<SlidesDbContext> options) : base(options)
        {
        }

        public DbSet<Presentation> Presentations { get; set; }

        public DbSet<Slide> Slides { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}