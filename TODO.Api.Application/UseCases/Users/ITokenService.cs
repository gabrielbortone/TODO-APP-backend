using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface ITokenService
    {
        string GenerateToken(string username, string userId);
        bool ValidateToken(JwtToken token);
        string GetUserIdFromToken(JwtToken token);
    }
}
