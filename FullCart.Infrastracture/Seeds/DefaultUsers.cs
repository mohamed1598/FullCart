using FullCart.Core.Consts;
using FullCart.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
        {
            AppUser admin = new()
            {
                UserName = "admin@FullCart.com",
                Email = "admin@FullCart.com",
                FullName = "admin",
                EmailConfirmed = true,
            };
            var user = await userManager.FindByNameAsync(admin.UserName);
            if (user is null)
            {
                await userManager.CreateAsync(admin, "P@ssword123");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
    }
}
