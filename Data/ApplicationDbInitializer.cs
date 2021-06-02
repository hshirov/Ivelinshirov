using Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var email = "admin@ivelinshirov.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "InitialPassword1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Any())
            {
                return;
            }

            var rolesNames = new List<string>
            {
                "Admin"
            };

            foreach (var roleName in rolesNames)
            {
                IdentityRole role = new IdentityRole(roleName);

                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
