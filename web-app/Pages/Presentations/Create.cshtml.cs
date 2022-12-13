using System.Threading.Tasks;
using DAL;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages.Presentations
{
    public class CreateModel : PageModel
    {
        private readonly PresentationDbContext _db;
        [BindProperty]
        public Presentation Presentation { get; set; }
        public CreateModel(PresentationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Presentations.AddAsync(Presentation);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
