namespace Entities.DataTransferObjects
{
    public record DirectorDto
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public IEnumerable<FilmDto> Films { get; set; }

    }
}
