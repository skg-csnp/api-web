using Csnp.Credential.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Csnp.Migrations.Credential.Seeds;

/// <summary>
/// Provides methods to seed initial roles and users for the Credential module.
/// </summary>
public static class SeedData
{
    #region -- Methods --

    /// <summary>
    /// Seeds default roles and the admin user into the Identity system.
    /// </summary>
    /// <param name="userManager">The <see cref="UserManager{TUser}"/> used to manage users.</param>
    /// <param name="roleManager">The <see cref="RoleManager{TRole}"/> used to manage roles.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task SeedAsync(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
    {
        // Seed Admin role if not exists
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new RoleEntity { Name = "Admin" });
        }

        // Seed Admin user if not exists
        UserEntity? adminUser = await userManager.FindByEmailAsync("admin@csnp.local");
        if (adminUser == null)
        {
            UserEntity user = new UserEntity
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
