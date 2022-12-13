namespace DAL
{
    using DAL.EntityModels;
    using Microsoft.EntityFrameworkCore;

    public class PresentationDbContext : DbContext
    {
        public PresentationDbContext(DbContextOptions<PresentationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Presentation> Presentations { get; set; }
    }
}
