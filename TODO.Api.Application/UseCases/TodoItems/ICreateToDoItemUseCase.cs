using TODO.Api.Application.DTOs;
using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public interface ICreateToDoItemUseCase
    {
        Task<(ToDoItemResume, FinalValidationResultDto)> Process(string userId, CreateToDoItemRequestDto request);
    }
}
