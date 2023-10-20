﻿using Microsoft.IdentityModel.Tokens;
using NewApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewApi.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(string key, string issuer, string audience, UserModel user) {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(sercurityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                                        audience: audience,
                                        claims: claims,
                                        expires: DateTime.Now.AddMinutes(10),
                                        signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }


    }
}
