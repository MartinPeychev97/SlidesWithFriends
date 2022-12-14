using BAL.Interfaces;
using DAL;
using DAL.EntityModels;
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

        public async Task<int> Add(int presentationId)
        {
            Slide slide = new Slide 
            {
                Title= "Title Slide",
                Text = "Super cool slide text",
                PresentationId = presentationId
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide.Id;
        }

        public async Task<Slide> Edit(int id, string title, string text)
        {
            var slite = await this.db.Slides.FindAsync(id);

            slite.Title = title;
            slite.Text = text;

            await this.db.SaveChangesAsync();

            return slite;
        }

        public async Task<Slide> GetById(int id) =>
            await this.db.Slides.FindAsync(id);
        

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
