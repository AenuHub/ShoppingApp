using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingApp.WebApi.Jwt
{
    public static class JwtHelper
    {
        public static string GenerateToken(JwtDto jwtDto)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtDto.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtClaimNames.Id, jwtDto.Id.ToString()),
                new Claim(JwtClaimNames.Role, jwtDto.Role.ToString()),
                new Claim(JwtClaimNames.Email, jwtDto.Email),
                new Claim(JwtClaimNames.FirstName, jwtDto.FirstName),
                new Claim(JwtClaimNames.LastName, jwtDto.LastName),

                new Claim(ClaimTypes.Role, jwtDto.Role.ToString())
            };

            var duration = DateTime.Now.AddMinutes(jwtDto.DurationInMinutes);
            var tokenDescriptor = new JwtSecurityToken(jwtDto.Issuer, jwtDto.Audience, claims, null, duration, credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
