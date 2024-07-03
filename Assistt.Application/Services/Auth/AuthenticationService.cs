using Assistt.Domain.Models;
using Assistt.Infrastructure.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string GenerateAccessToken(string email)
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


        public RefreshToken GenerateRefreshTokenAsync(int userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.Now.AddDays(7),
                
            };

             _unitOfWork.RefreshTokens.Create(refreshToken);
             _unitOfWork.Commit();

            return refreshToken;
        }

        public string RefreshTokenAsync(string token)
        {
            var refreshToken = _unitOfWork.RefreshTokens.FirstOrDefault(x => x.Token == token);

            if (refreshToken == null)
            {
                return null;
            }

            if (refreshToken.IsRevoked)
            {
                return null;
            }

            if (refreshToken.Expires < DateTime.Now)
            {
                return null;
            }

            var user = _unitOfWork.Users.GetById(refreshToken.UserId);

            if (user == null)
            {
                return null;
            }

            refreshToken.IsUsed = true;
            refreshToken.Used = DateTime.Now;
            refreshToken.Expires = DateTime.Now.AddDays(7);

            _unitOfWork.Commit();


            var newAccessToken = GenerateAccessToken(user.Email);

            return newAccessToken;
        }

    }


    public interface IAuthenticationService
    {
        string GenerateAccessToken(string email);
        RefreshToken GenerateRefreshTokenAsync(int userId);
        string RefreshTokenAsync(string token);
    }
}
