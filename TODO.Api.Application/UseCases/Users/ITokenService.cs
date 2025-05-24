namespace TODO.Api.Application.UseCases.Users
{
    public interface ITokenService
    {
        string GenerateToken(string username, Guid userId);
        bool ValidateToken(string token);
    }
}
