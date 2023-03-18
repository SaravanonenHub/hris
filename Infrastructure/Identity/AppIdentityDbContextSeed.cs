using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Saravanan Nanjundan",
                    UserName = "3434",
                    Email = "s@gmail.com",

                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}