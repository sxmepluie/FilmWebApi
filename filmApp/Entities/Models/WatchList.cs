namespace Entities.Models
{
    public class WatchList
    {
        public int WatchListId { get; set; }
        public int FilmId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Film Film { get; set; }
    }
}


