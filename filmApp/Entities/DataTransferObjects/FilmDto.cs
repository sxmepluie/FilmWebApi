namespace Entities.DataTransferObjects
{
    public record FilmDto
    {
        public int FilmId { get; init; }
        public string FilmTitle { get; init; }
        public string Description { get; init; }
        public int Year { get; init; }
        public int Time { get; init; }
        public double Rate { get; init; }
        public int DirectorId { get; init; }
        public string DirectorName { get; init; }
    }
}
