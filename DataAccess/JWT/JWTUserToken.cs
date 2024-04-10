using BusinessObject.Models;
using DataAccess.ViewModels.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.JWT
{
    public class JWTUserToken
    {
        public static string GenerateJWTTokenUser(UserTokenViewModel user)
        {
            JwtSecurityToken tokenUser = null;
            tokenUser = new JwtSecurityToken(
                issuer: "https://trototweb.com",
                audience: "https://trototweb.com",
                claims: new[] {
                 //Id
                 new Claim("UserId", user.UserId.ToString()),
                 //Username
                 new Claim("Username", user.FullName),
                 //Email
                 new Claim("Email", user.Email),
                 //Role
                 new Claim ("Role", user.Role),
                 //Status
                 new Claim("Status", user.Status.ToString()),
                },
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TroTotWeb2023ForFPTU")),
                        algorithm: SecurityAlgorithms.HmacSha256
                        )
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenUser);
        }
    }
}
