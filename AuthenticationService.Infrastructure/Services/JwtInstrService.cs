using AuthenticationService.Application.ServiceInterfaces;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Services
{
    public class JwtInstrService : IJwtInstrService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtExpirationMinutes;
        public string GenerateJwtToken(Instructor instructor)
        {
            if (instructor == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
            var securityKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            if (string.IsNullOrEmpty(securityKey))
            {
                throw new Exception("Jwt Secret key is Missing");
            }

            var key = Encoding.UTF8.GetBytes(securityKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
       {
        new Claim(ClaimTypes.NameIdentifier, instructor.Instructor_Id.ToString()),
        new Claim(ClaimTypes.Name, instructor.Email),
        new Claim(ClaimTypes.Role,"Instructor")
    };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
