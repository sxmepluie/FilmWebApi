using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Username is Required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        public string? Password { get; set; }
    }
}
