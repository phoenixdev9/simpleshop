using System.Text;
using Domains.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions {
    public static class IdentityServiceExtensions {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services, IConfiguration config) {
            var builder = services.AddIdentityCore<User> ();

            builder = new IdentityBuilder (builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<StoreContext> ();
            builder.AddSignInManager<SignInManager<User>> ();

            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    };
                });

            return services;
        }
    }
}