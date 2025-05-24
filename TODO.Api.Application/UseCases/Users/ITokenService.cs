using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface ITokenService
    {
        string GenerateToken(string username, Guid userId);
        bool ValidateToken(JwtToken token);
        Guid GetUserIdFromToken(JwtToken token);
    }
}
