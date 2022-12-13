using BAL.Interfaces;
using DAL;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SlideService : ISlideService
    {
        private readonly SlidesDbContext db;

        public SlideService(SlidesDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Add(string title, string text, int presentationId)
        {
            Slide slide = new Slide 
            {
                Title= title,
                Text = text,
                PresentationId = presentationId
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide.Id;
        }

        public async Task<Slide> GetById(int id) =>
            await this.db.Slides.Include(x => x.Presentation).FirstOrDefaultAsync(x => x.Id == id);
        

        public async Task<bool> Remove(int id)
        {
            var slide = await this.db.Slides.FindAsync(id);

            if (slide == null) 
            {
                return false;
            }

            this.db.Slides.Remove(slide);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
