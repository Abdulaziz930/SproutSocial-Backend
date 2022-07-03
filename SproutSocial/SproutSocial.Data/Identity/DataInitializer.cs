using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Data.Identity
{
    public class DataInitializer
    {
        public static void SeedData(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            foreach (var role in Enum.GetValues(typeof(RoleConstants.RoleType)))
            {
                var isExist = roleManager.RoleExistsAsync(role.ToString()).Result;
                if (!isExist)
                {
                    roleManager.CreateAsync(new IdentityRole { Name = role.ToString() }).Wait();
                }
            }

            var user = new AppUser
            {
                Fullname = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                IsActive = true
            };

            var existUser = userManager.FindByNameAsync("Admin").Result;
            if (existUser == null)
            {
                userManager.CreateAsync(user, "Admin@123").Wait();
                userManager.AddToRoleAsync(user, RoleConstants.RoleType.Admin.ToString()).Wait();
            }
        }
    }
}
