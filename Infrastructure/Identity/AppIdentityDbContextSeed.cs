using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities.Employees;
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
                var appUserData = File.ReadAllText("../Infrastructure/Data/SeedData/employee.json");
                var appUsers = JsonSerializer.Deserialize<List<Employee>>(appUserData);
                foreach (var user in appUsers)
                {
                    var addUser = new AppUser
                    {
                        DisplayName = user.DisplayName,
                        UserName = user.EmployeeCode,
                        Email = user.EmailID
                    };
                    await userManager.CreateAsync(addUser
                        , $"Mil@cr0n_{user.EmployeeCode}");
                    await userManager.AddToRoleAsync(
                        addUser, user.TeamRole);
                }
                
               
            }
            
        }
        public static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roleData = File.ReadAllText("../Infrastructure/Data/SeedData/identityRole.json");
                var roles = JsonSerializer.Deserialize<List<AppRole>>(roleData);
                foreach(var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
               
            }

        }
    }
}