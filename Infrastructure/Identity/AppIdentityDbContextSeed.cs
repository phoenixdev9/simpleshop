using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "104st 41a ave",
                        City = "Vancouver",
                        Province = "BC",
                        PostalCode = "V1N4L3"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}