using DannyKeyboard.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Common
{
    public class JwtAuthentication
    {
        private readonly IConfiguration _configure;
        public JwtAuthentication(IConfiguration configure)
        {
            _configure = configure;
        }

        public string GenerateAccessToken(User user)
        {
            var jwtKey = _configure["JwtSecret"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.Trim()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString().Trim()),
            };

            var token = new JwtSecurityToken(
                issuer: _configure["JWT:Issuer"],
                audience: _configure["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credential
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        //public RefreshTokenResponse RefreshTokenAsync(User user)
        //{
        //    var newAccessToken = GenerateAccessToken(user);
        //    var newRefreshToken = GenerateRefreshToken();

        //    return new RefreshTokenResponse
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    };
        //}
    }
}
