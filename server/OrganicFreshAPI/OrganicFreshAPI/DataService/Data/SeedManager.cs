using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Data;

public static class SeedManager
{
    public static async Task Seed(IServiceProvider services)
    {
        await SeedRoles(services);
        await SeedAdminUser(services);
    }

    private static async Task SeedRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await roleManager.CreateAsync(new IdentityRole(Role.Admin));
        await roleManager.CreateAsync(new IdentityRole(Role.User));
    }

    private static async Task SeedAdminUser(IServiceProvider services)
    {
        var context = services.GetRequiredService<MyDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "admin");

        if (adminUser == null)
        {
            adminUser = new User { Name = "AuthenticationAdmin", Email = "admin@mail.com" };
            await userManager.CreateAsync(adminUser, "Admin-123");
            await userManager.AddToRoleAsync(adminUser, Role.Admin);
        }
    }
}