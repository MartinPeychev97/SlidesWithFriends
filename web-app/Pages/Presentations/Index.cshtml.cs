using System.Collections;
using System.Collections.Generic;
using DAL;
using DAL.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app.Pages.Presentations
{
    public class IndexModel : PageModel
    {
        private readonly SlidesDbContext _db;
        public IEnumerable<DAL.EntityModels.Presentation> Presentations { get; set; }
        public IndexModel(SlidesDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Presentations = _db.Presentations;
        }
    }
}
