using BAL.Interfaces;
using DAL;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class PresentationService : IPresentationService
    {
        private readonly SlidesDbContext db;

        public PresentationService(SlidesDbContext db)
        {
            this.db = db;
        }

        public async Task Create(string name, string userId)
        {
            var presentation = new Presentation
            {
                Name = name,
                UserId = userId
            };

            await this.db.Presentations.AddAsync(presentation);
            await this.db.SaveChangesAsync();
        }

        public async Task<Presentation> GetById(int presentationId) =>
            await this.db.Presentations
                .Where(p => p.Id == presentationId)
                .Include(p => p.Slides)
                .FirstOrDefaultAsync();

        public async Task<bool> EditName(int id, string name)
        {
           var presentation = await this.GetById(id);

            if (presentation == null)
            {
                return false;
            }

            presentation.Name = name;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(int presentationId)
        {
            var presentation = await this.db.Presentations.FindAsync(presentationId);

            if (presentation == null)
            {
                return false;
            }

            this.db.Presentations.Remove(presentation);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Presentation>> GetAll(string userId) =>
            await this.db.Presentations
                .Where(p => p.UserId == userId)
                .Include(p => p.Slides).ToListAsync();
    }
}
