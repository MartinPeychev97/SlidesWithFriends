using DAL;
using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Presentation> GetById(int id) =>
            await this.db.Presentations
                .Include(p => p.Slides)
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}
