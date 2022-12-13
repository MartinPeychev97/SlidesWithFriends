using System.Threading.Tasks;
using DAL;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages.Presentations
{
    public class DeleteModel : PageModel
    {
        private readonly PresentationDbContext _db;
        [BindProperty]
        public Presentation Presentation { get; set; }
        public DeleteModel(PresentationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Presentation = _db.Presentations.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var presentation = _db.Presentations.Find(Presentation.Id);
            if (presentation != null)
            {       
                _db.Presentations.Remove(presentation);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
