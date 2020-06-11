using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domains.Entities;
using Domains.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services {
    public class Token : IToken {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public Token (IConfiguration config) {
            _config = config;
            _key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Token:Key"]));
        }

        public string CreateToken (User user) {
            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim (JwtRegisteredClaimNames.GivenName, user.DisplayName)
            };

            var creds = new SigningCredentials (_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            return tokenHandler.WriteToken (token);
        }
    }
}