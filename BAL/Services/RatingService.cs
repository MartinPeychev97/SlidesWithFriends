using BAL.Interfaces;
using DAL;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class RatingService : IRatingService
    {
        private readonly SlidesDbContext db;
        public RatingService(SlidesDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Rating>> GetAll(int presentationId) =>
            await this.db.Ratings.Where(s => s.PresentationId == presentationId).ToListAsync();

        public async Task<Rating> GetById(int id) =>
            await this.db.Ratings.Include(x => x.Id).FirstOrDefaultAsync(x => x.Id == id);


        public int CalculateAverageRating(int presentationId)
        {
            var count = db.Ratings.Count(e => e.PresentationId == presentationId && e.value >= 0);
            if (count == 0) return 0;

            return db.Ratings.Where(e => e.PresentationId == presentationId && e.value >= 0).Select(e => e.value).Sum() / count;
        }

        public async Task<bool> ClearVotes(int presentationId)
        {

            foreach (var item in  this.db.Ratings.Where(e => e.PresentationId == presentationId).ToList())
            {
                this.db.Ratings.Remove(item);
            }
            this.db.SaveChanges();

            return true;
        }

        public async Task<Rating> AddRating(int presentationId, int ratingValue, string userId)
        {
            Rating rating = new Rating
            {
                PresentationId = presentationId,
                value = ratingValue,
                UserId= userId
            };

            await this.db.Ratings.AddAsync(rating);
            await this.db.SaveChangesAsync();

            return rating;
        }

        public async Task<bool> EditRating(int id, int ratingValue)
        {
            var rating = await this.db.Ratings.FindAsync(id);

            if (rating is null)
            {
                return false;
            }

            rating.value = ratingValue;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(int ratingId)
        {
            var rating = await this.db.Ratings.FindAsync(ratingId);

            if (rating == null)
            {
                return false;
            }

            this.db.Ratings.Remove(rating);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
