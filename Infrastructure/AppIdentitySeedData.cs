using Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppIdentitySeedData
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Amina",
                    Email = "amina@gmail.com",
                    UserName = "AminaRamadan",
                    Address = new Address
                    {
                        FirstName = "Amina",
                        LastName = "Ramadan",
                        Street = "9street",
                        State = "Maadi",
                        City = "Cairo",
                        ZipCode = "105324"
                    }
                };
                await userManager.CreateAsync(user , "Password123!");
            }
        }
    }
}
