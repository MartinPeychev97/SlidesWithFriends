using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        //public decimal AverageRating { get; set; }
        public int value { get; set; }
        public string UserId { get; set; }
        public int PresentationId { get; set; }
    }
}
