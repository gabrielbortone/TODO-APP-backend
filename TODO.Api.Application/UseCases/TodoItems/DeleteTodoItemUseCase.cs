using TODO.Api.Application.DTOs;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoItemUseCase(ITodoRepository todoRepository)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<FinalValidationResultDto> Process(string userId, Guid todoItemId)
        {
            var validationResult = new FinalValidationResultDto();
            var result = _repository.DeleteAsync(todoItemId, userId);

            if (result == null || !await result)
            {
                validationResult.AddError("Delete", "Failed to delete the todo item.", "DeleteFailed");
                return validationResult;
            }

            validationResult.IsValid = true;
            return validationResult;
        }
    }
}
