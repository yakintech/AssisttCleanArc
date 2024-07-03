using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public string GenerateToken(string email)
        {
          var tokenHandler = new JwtSecurityTokenHandler();


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ironmaidenpentagramslipknotironmaidenpentagramslipknot"));

            //token sifreleme algoritmasi

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("email", email)
                }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credentials,
                Issuer = "Asist",
                Audience = "Asist"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenData = tokenHandler.WriteToken(token);

            return tokenData;

        }
    }


    public interface IAuthenticationService
    {
        string GenerateToken(string email);
    }
}
