using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record FilmDtoForUpdate : FilmDtoForManipulation
    {
        [Required]
        public int FilmId { get; set; }
    }
}
