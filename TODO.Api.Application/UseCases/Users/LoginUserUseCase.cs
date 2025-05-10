using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODO.Api.Application.AppSettings;
using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly JwtAuthConfiguration _config;
        public LoginUserUseCase(IOptions<JwtAuthConfiguration> options)
        {
            _config = options.Value;
        }

        public async Task<(FinalValidationResultDto, TokenJwtResultDto)> Process(string userName, string password)
        {


            throw new NotImplementedException();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
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
    }
}
