using TODO.Api.Application.DTOs;
using TODO.Api.Domain.Entities;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public class CreateToDoItemUseCase : ICreateToDoItemUseCase
    {
        private readonly ITodoRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateToDoItemUseCase(
            ITodoRepository todoRepository,
            ICategoryRepository categoryRepository)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<(ToDoItemResume, FinalValidationResultDto)> Process(
            string userId, CreateToDoItemRequestDto request)
        {
            var validationResult = new FinalValidationResultDto();
            
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                validationResult.AddError("CategoryNotFound", "The specified category does not exist.", "CategoryNotFoundError");
                return (null, validationResult);
            }

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

            insertResult.CategoryName = category.Name;
            insertResult.CategoryDescription = category.Description;

            return (insertResult, validationResult);
        }
    }
}
