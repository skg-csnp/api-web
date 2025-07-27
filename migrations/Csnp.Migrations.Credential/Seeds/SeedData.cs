using Microsoft.AspNetCore.Identity;

namespace Csnp.Migrations.Credential.Seeds;

public static class SeedData
{
    #region -- Methods --

    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Seed Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new Role { Name = "Admin" });
        }

        // Seed Admin User
        var adminUser = await userManager.FindByEmailAsync("admin@csnp.local");
        if (adminUser == null)
        {
            var user = new User
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
