using Csnp.Credential.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Csnp.Migrations.Credential.Seeds;

public static class SeedData
{
    #region -- Methods --

    public static async Task SeedAsync(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
    {
        // Seed Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new RoleEntity { Name = "Admin" });
        }

        // Seed Admin User
        UserEntity? adminUser = await userManager.FindByEmailAsync("admin@csnp.local");
        if (adminUser == null)
        {
            var user = new UserEntity
            {
                UserName = "admin",
                Email = "admin@csnp.local",
                DisplayName = "System Admin"
            };

            await userManager.CreateAsync(user, "Toan@123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    #endregion
}
