using BAL.Interfaces;
using DAL;
using DAL.EntityModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class PresentationService : IPresentationService
    {
        private readonly SlidesDbContext db;
        private readonly IWebHostEnvironment hostEnvironment;

        public PresentationService(SlidesDbContext db, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task Create(string name, string userId, string image)
        {
            var presentation = new Presentation
            {
                Name = name,
                UserId = userId,
                Image = image
            };
            var user = await this.db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            user.Presentations.Add(presentation);
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

        public async Task<bool> EditImage(int id, string image)
        {
            var presentation = await this.GetById(id);

            if (presentation == null)
            {
                return false;
            }

            RemoveImage(presentation);

            presentation.Image = image;

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

            RemoveImage(presentation);

            this.db.Presentations.Remove(presentation);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Presentation>> GetAll(string userId) =>
            await this.db.Presentations
                .Where(p => p.UserId == userId)
                .Include(p => p.Slides).ToListAsync();

        private void RemoveImage(Presentation presentation)
        {
            if (presentation.Image != @"\images\presentation\default.png")
            {
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, presentation.Image.TrimStart('\\'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }
    }
}
