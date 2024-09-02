namespace Entities.DataTransferObjects
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }


        public int FilmId { get; set; }
        public string FilmTitle { get; set; }
    }
}
