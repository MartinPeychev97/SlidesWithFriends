using DAL.EntityModels;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Models.Slide
{
    public class SlideEventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }

        public string Background { get; set; }

        public int Rating { get; set; }

        public string Type { get; set; }
    }
}
