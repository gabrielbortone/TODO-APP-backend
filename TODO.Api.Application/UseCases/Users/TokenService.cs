using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODO.Api.Application.AppSettings;
using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthConfiguration _config;
        public TokenService(
            IOptions<JwtAuthConfiguration> options)
        {
            _config = options.Value;
        }

        public string GenerateToken(string username, Guid userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_config.ExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GetUserIdFromToken(JwtToken token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (this.ValidateToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token.Token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
                if (userIdClaim == null)
                {
                    throw new Exception("User ID claim not found in token.");
                }

                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new Exception("Invalid User ID claim in token.");
                }

                return userId;
            }
            else
            {
                throw new Exception("Invalid token.");
            }
            
        }

        public bool ValidateToken(JwtToken token)
        {
            var result = new JwtSecurityTokenHandler().ValidateToken(token.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret)),
                ValidateIssuer = true,
                ValidIssuer = _config.Issuer,
                ValidateAudience = true,
                ValidAudience = _config.Audience,
                ValidateLifetime = true,
            }, out SecurityToken validatedToken);

            return result != null && validatedToken != null;
        }
    }
}
