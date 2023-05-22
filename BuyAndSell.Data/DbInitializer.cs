using BuyAndSell.Data.Entities;
using BuyAndSell.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BuyAndSell.Data
{
    public class DbInitializer
    {
        private readonly DatabaseContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(UserManager<User> userManager, DatabaseContext context,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            if (!_context.Users.Any())
            {
                var roles = Enum.GetNames(typeof(RoleEnum)).ToList();

                foreach (var role in roles)
                    await _roleManager.CreateAsync(new Role { Name = role });
               
                var user = new User
                {
                    Email = "admin@buy-and-sell.com",
                    UserName = "admin@buy-and-sell.com",
                    FirstName = "Admin",
                    LastName = "Admin",                    
                };
                var result = await _userManager.CreateAsync(user, "String123");
                if (result.Succeeded)
                {
                    foreach(var role in roles)
                        await _userManager.AddToRoleAsync(user, role);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
