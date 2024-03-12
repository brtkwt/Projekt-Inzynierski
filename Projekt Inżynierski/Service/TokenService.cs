using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Interfaces;
using StackExchange.Redis;

namespace Projekt_Inżynierski.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _securityKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigninKey"]));
        }

        public string CreateJWTToken(AppUser appUser, string roles)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName, appUser.GivenName),
                new Claim(ClaimTypes.Role, roles )
            };

            var encryption = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = encryption,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}