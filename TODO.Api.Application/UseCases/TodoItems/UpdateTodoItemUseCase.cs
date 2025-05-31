using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Api.Application.DTOs;
using TODO.Api.Domain.Entities;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public class UpdateTodoItemUseCase : IUpdateTodoItemUseCase
    {
        private readonly ITodoRepository _repository;
        public UpdateTodoItemUseCase(ITodoRepository todoRepository)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<FinalValidationResultDto> Process(string userId, UpdateToDoItemRequestDto request)
        {
            var validationResult = new FinalValidationResultDto();
            var todoItemDb = await _repository.GetByIdAsync(request.Id, userId);

            if (todoItemDb == null)
            {
               validationResult.AddError("Id","Todo item not found.","TodoItemNotFound");
                return validationResult;
            }

            todoItemDb.Update(request.Title, request.Description, (Priority)request.Priority,
                request.DueDate, request.CategoryId);

            if (todoItemDb.FinishDate.HasValue || request.IsCompleted)
            {
                todoItemDb.MarkAsCompleted();
            }
            else
            {
                todoItemDb.MarkAsNotCompleted();
            }

            var updateResult = await _repository.UpdateAsync(todoItemDb);
            if (updateResult == null || !updateResult)
            {
                validationResult.AddError("Update", "Failed to update the todo item.", "UpdateFailed");
                return validationResult;
            }

            validationResult.IsValid = true;
            return validationResult;

        }
    }
}
