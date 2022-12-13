using DAL.EntityModels;
using DAL.EntityModels.TestEntity;
using DAL.EntityModels.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SlidesDbContext : IdentityDbContext<SlidesUser>
    {
        public SlidesDbContext() : base()
        {
        }

        public SlidesDbContext(DbContextOptions<SlidesDbContext> options) : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }

        public DbSet<Presentation> Presentations { get; set; }

        public DbSet<Slide> Slides { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TestEntity>()
                .HasData(
                new TestEntity()
                {
                    Id = 1,
                    Name = "firstTestEntity"
                },
                new TestEntity()
                {
                    Id = 2,
                    Name = "secondTestEntity"
                }
                );

            base.OnModelCreating(builder);
        }
    }
}