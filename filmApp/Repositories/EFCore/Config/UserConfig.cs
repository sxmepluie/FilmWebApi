using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var passwordHasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()

            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin123456");
            var editor = new User
            {
                Id = Guid.NewGuid(),
                UserName = "editor",
                NormalizedUserName = "EDITOR",
                Email = "editor@editor.com",
                NormalizedEmail = "EDITOR@EDITOR.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            editor.PasswordHash = passwordHasher.HashPassword(editor, "editor123456");

            builder.HasData(admin, editor);
        }
    }
}
