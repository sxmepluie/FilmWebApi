namespace Entities.DataTransferObjects
{
    public class ReviewDtoForInsertion
    {
        public string Title { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public int FilmId { get; set; }
    }
}
