namespace DAL.EntityModels
{
    public class Rating
    {
        public int Id { get; set; }
        public int value { get; set; }
        public string UserId { get; set; }
        public int PresentationId { get; set; }
    }
}
