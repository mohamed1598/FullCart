using FullCart.Core.Entities;
using FullCart.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;

namespace FullCart.API.Extensions
{
    public static class UserSeedExtensions
    {
        public async static Task<WebApplication> SeedUsers(this WebApplication app)
        {
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            await DefaultRoles.SeedRolesAsync(roleManager);
            await DefaultUsers.SeedAdminUserAsync(userManager);
            return app;
        }
    }
}
