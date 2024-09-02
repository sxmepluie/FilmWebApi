namespace Entities.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string FilmTitle { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Time { get; set; }
        public double Rate { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
