using BAL.Interfaces;
using Common.InputModels.Presentation;
using DAL;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Presentation> GetById(int id) =>
            await this.db.Presentations
                .Include(p => p.Slides)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task EditBackground(PresentationBackgroundInputModel model)
        {
            var presentation = await this.GetById(model.PresentationId);

            if (presentation == null)
            {
                throw new ArgumentNullException("Presentation does not exist");
            }

            //presentation.Background = model.Background;

            await this.db.SaveChangesAsync();
        }

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
    }
}
