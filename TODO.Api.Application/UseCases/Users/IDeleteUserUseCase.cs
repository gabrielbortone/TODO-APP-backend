using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface IDeleteUserUseCase
    {
        Task<FinalValidationResultDto> Process(Guid id);
    }
}
