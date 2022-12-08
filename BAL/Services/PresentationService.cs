using DAL;
using DAL.EntityModels;
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

        public async Task<Presentation> GetById(int id)
        {
            var presentation = await this.db.Presentations.FindAsync(id);
            var slides = this.db.Slides.Where(s => s.PresentationId == presentation.Id).ToList();

            presentation.Slides = slides;

            return presentation;
        }
    }
}
