using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public record UserDtoForInsertion
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; init; }
        public ICollection<string> Roles { get; init; }

    }
}
