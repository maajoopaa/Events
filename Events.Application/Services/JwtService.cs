using Events.Application.Services.AccountService;
using Events.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Services
{
    public class JwtService
    {
        public static string GenerateToken(Participant participant)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, participant.Email),
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
