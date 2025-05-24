using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface IGetUserUseCase
    {
        Task<(FinalValidationResultDto, UserResumeDto)> Process(string userId);
    }
}
