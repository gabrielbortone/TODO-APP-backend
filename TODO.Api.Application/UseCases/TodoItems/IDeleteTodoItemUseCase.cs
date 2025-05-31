using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public interface IDeleteTodoItemUseCase
    {
        Task<FinalValidationResultDto> Process(string userId, Guid todoItemId);
    }
}
