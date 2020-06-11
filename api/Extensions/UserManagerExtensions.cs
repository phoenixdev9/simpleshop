using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByUserByClaimsPrincipleWithAddressAsync(this UserManager<User> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.address).Include(x => x.BasketItems).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<User> FindByEmailFromClaimsPrinciple(this UserManager<User> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.address).Include(x => x.BasketItems).Include("BasketItems.Product").SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}