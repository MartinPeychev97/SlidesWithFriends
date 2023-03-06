using BAL.Interfaces;
using BAL.Models.Slide;
using DAL;
using DAL.EntityModels;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Slide>> GetAll(int presentationId) =>
            await this.db.Slides.Where(s => s.PresentationId == presentationId).ToListAsync();

        public async Task<Slide> GetById(int id) =>
            await this.db.Slides.Include(x => x.Presentation).FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Slide> AddTitleSlide(int presentationId)
        {
            Slide slide = new Slide
            {
                Title = "Title Slide",
                Text = "Super cool slide text",
                Type = SlideType.Title,
                PresentationId = presentationId
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide;
        }

        public async Task<Slide> AddImageSlide(int presentationId, string image)
        {
            Slide slide = new Slide
            {
                Image = image,
                Text = "Image description here",
                Type = SlideType.Image,
                PresentationId = presentationId
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide;
        }

        public async Task<bool> EditTitle(int id, string title)
        {
            var slide = await this.db.Slides.FindAsync(id);

            if (slide is null)
            {
                return false;
            }

            slide.Title = title;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditText(int id, string text)
        {
            var slide = await this.db.Slides.FindAsync(id);

            if (slide is null)
            {
                return false;
            }

            slide.Text = text;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditOnDragAndDrop(int firstId, int secondId)
        {
            var firstSlide = await this.db.Slides.FindAsync(firstId);

            if (firstSlide is null)
            {
                return false;
            }
            var secondSlide = await this.db.Slides.FindAsync(secondId);

            if (secondSlide is null)
            {
                return false;
            }

            var firstTemp = new Slide
            {
                Title = firstSlide.Title,
                Text = firstSlide.Text,
                Image = firstSlide.Image,
                Type = firstSlide.Type,
                Background = firstSlide.Background,
                Rating = firstSlide.Rating,
            };

            firstSlide.Title = secondSlide.Title;
            firstSlide.Text = secondSlide.Text;
            firstSlide.Image = secondSlide.Image;
            firstSlide.Background = secondSlide.Background;
            firstSlide.Type = secondSlide.Type;
            firstSlide.Rating = secondSlide.Rating;

            secondSlide.Title = firstTemp.Title;
            secondSlide.Text = firstTemp.Text;
            secondSlide.Image = firstTemp.Image;
            secondSlide.Background = firstTemp.Background;
            secondSlide.Type = firstTemp.Type;
            secondSlide.Rating = firstTemp.Rating;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(int slideId)
        {
            var slide = await this.db.Slides.FindAsync(slideId);

            if (slide == null)
            {
                return false;
            }

            this.db.Slides.Remove(slide);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task EditBackground(EditSlideBackgroundInputModel model)
        {
            var slide = await this.db.Slides.FindAsync(model.Id);

            slide.Background = model.Background;

            await this.db.SaveChangesAsync();
        }

        public async Task<Slide> AddRatingSlide(int presentationId, int rating)
        {
            Slide slide = new Slide
            {
                Type = SlideType.Rating,
                PresentationId = presentationId,
                Rating = rating,
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide;
        }

        public async Task<Slide> AddWordCloudSlide(int presentationId)
        {
            Slide slide = new Slide
            {
                Title = "WordCloud",
                Text = "Answer Box",
                Type = SlideType.WordCloud,
                PresentationId = presentationId
            };

            await this.db.Slides.AddAsync(slide);
            await this.db.SaveChangesAsync();

            return slide;
        }


    }
}