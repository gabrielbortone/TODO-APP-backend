using TODO.Api.Application.DTOs;
using TODO.Api.Domain.Entities;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public class CreateToDoItemUseCase : ICreateToDoItemUseCase
    {
        private readonly ITodoRepository _repository;
        public CreateToDoItemUseCase(ITodoRepository todoRepository)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<(ToDoItemResume, FinalValidationResultDto)> Process(
            string userId, CreateToDoItemRequestDto request)
        {
            var validationResult = new FinalValidationResultDto();
            
            var todoItem = TodoItem.Create(
                request.Title, 
                request.Description, 
                (Priority)request.Priority, 
                request.DueDate, 
                request.CategoryId);

            var insertResult = await _repository.AddAsync(todoItem, userId);

            if (insertResult == null)
            {
                validationResult.AddError("CreateToDoItemError", "Failed to create the ToDo item.", "ErrorCreateTodoItem");
                return (null, validationResult);
            }

            return (insertResult, validationResult);
        }
    }
}
