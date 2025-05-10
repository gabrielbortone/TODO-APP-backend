using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface IUpdateUserUseCase
    {
        Task<FinalValidationResultDto> Process(UpdateUserDto updateUser);
    }
}
