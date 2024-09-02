using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Config
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (!await roleManager.RoleExistsAsync("ADMIN"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "ADMIN", NormalizedName = "ADMIN" });
            }
            if (!await roleManager.RoleExistsAsync("EDITOR"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "EDITOR", NormalizedName = "EDITOR" });
            }

            var admin = await userManager.FindByNameAsync("admin");
            if (admin != null)
            {
                await userManager.AddToRoleAsync(admin, "ADMIN");
            }

            var editor = await userManager.FindByNameAsync("editor");
            if (editor != null)
            {
                await userManager.AddToRoleAsync(editor, "EDITOR");
            }
        }
    }
}
