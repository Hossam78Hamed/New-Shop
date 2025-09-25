using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Accounts;
using Shop.Core.Services.AccountServices;
namespace Shop.Core.Services.EmailServices
{
    public class GenerateToken : IGenerateToken
    {
        
        private readonly IConfiguration configuration;
        public GenerateToken(IConfiguration _configuration ) {
            configuration = _configuration;
        }

        public string GetAndCreateToken(ApplicationUser applicationUser)
        {
            List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.Email,applicationUser.Email),
          new Claim(ClaimTypes.Name,applicationUser.UserName)

            };
            string secret = configuration["Token:Secret"];

            if (string.IsNullOrEmpty(secret)) {
                throw new ArgumentNullException("Token secret is not configured");

            }

            byte[] key = Encoding.ASCII.GetBytes(secret);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1), // Adjust token expiration time as needed
                Issuer = configuration["Token:Issuer"],
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(token);

            // Log the token to ensure it looks correct
            //Console.WriteLine("Generated JWT: " + tokenString);

            return tokenString;
           


        }
    }
}
