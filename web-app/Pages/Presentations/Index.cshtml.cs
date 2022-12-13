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
        private readonly PresentationDbContext _db;
        public IEnumerable<Presentation> Presentations { get; set; }
        public IndexModel(PresentationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Presentations = _db.Presentations;
        }
    }
}
