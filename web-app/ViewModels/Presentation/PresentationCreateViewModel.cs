using System.ComponentModel.DataAnnotations;

namespace web_app.ViewModels.Presentation
{
    public class PresentationCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
