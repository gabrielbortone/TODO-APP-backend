using TODO.Api.Application.DTOs;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public class GetTodoUserCase : IGetTodoUserCase
    {
        private readonly ITodoRepository _todoRepository;
        public GetTodoUserCase(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<GetItemsResultDto<ToDoItemResume>> Process(string userId, TodoQueryParametersDto todoQueryParameters)
        {
            var result = new GetItemsResultDto<ToDoItemResume>();
            
            try
            {
                var (todoItems, totalPages, totalItems) = await _todoRepository.GetAllAsync(
                    userId,
                    todoQueryParameters.Search,
                    todoQueryParameters.OrderBy,
                    todoQueryParameters.OrderDirection,
                    todoQueryParameters.Priority,
                    todoQueryParameters.DueDate,
                    todoQueryParameters.FinishDate,
                    todoQueryParameters.IncludeCompleted,
                    todoQueryParameters.Page,
                    todoQueryParameters.ItemsPerPage);

                result.SetData(todoItems);
                result.SetPagination(totalPages, totalItems, todoQueryParameters.Page, todoQueryParameters.ItemsPerPage);

            }
            catch (Exception ex)
            {
                result.ValidationResult.AddError("GetTodoItems","An error occurred while processing the request.", "ErrorEntityFrameworkGetItems");
            }

            return result;
        }
    }
}
