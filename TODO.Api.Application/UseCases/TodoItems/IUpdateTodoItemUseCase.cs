using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public interface IUpdateTodoItemUseCase
    {
        Task<FinalValidationResultDto> Process(string userId, UpdateToDoItemRequestDto request);
    }
}
