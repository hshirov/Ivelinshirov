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
            if (userManager.Users.Any())
            {
                return;
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "admin@ivelinshirov.com",
                    Email = "admin@ivelinshirov.com",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    UserName = "owner@ivelinshirov.com",
                    Email = "owner@ivelinshirov.com",
                    EmailConfirmed = true
                }
            };

            foreach(var user in users)
            {
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
