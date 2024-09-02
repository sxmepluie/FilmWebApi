using Microsoft.AspNetCore.Identity;


namespace Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        public String? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
