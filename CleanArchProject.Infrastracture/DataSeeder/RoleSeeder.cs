using CleanArchProject.Data.Entities.Identies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Infrastracture.DataSeeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.AnyAsync();
            if (!rolesCount)
            {

                await _roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                });
            }
        }

    }
}
