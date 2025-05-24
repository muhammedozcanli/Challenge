using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Challenge.Common.Security
{
    public static class TokenHelper
    {
        private static string _secretKey = "x49f$J3qM@r!zW7uQ9sDpL^8Nc#2eTbY";
        private static string _issuer = "challenge-api";
        private static string _audience = "challenge-client";
        private static int _expirationMinutes = 60;

        /// <summary>
        /// Generates a JWT (JSON Web Token) for the specified user with given claims.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="firstName">The first name of the user, included in the token claims.</param>
        /// <returns>A signed JWT as a string.</returns>
        public static string GenerateToken(Guid userId, string firstName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, firstName),
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
} 