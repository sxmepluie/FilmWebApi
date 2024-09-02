namespace Entities.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public User User { get; set; }

        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
