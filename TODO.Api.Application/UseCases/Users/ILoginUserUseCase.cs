using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface ILoginUserUseCase
    {
        public Task<(FinalValidationResultDto, TokenJwtResultDto)> Process(string userName, string password);
    }
}
