using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract record FilmDtoForManipulation
    {

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public String FilmTitle { get; init; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public String Description { get; init; }

        [Required]
        [Range(1500,2024)]
        public int Year { get; init; }

        [Required]
        [Range(60,360 )]
        public int Time { get; init; }

        [Required]
        public int DirectorId { get; init; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string DirectorName { get; init; }

    }
}
