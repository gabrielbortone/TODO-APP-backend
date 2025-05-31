using TODO.Api.Application.DTOs;
using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Application.UseCases.TodoItems
{
    public interface IGetTodoUserCase
    {
        Task<GetItemsResultDto<ToDoItemResume>> Process(string userId, TodoQueryParametersDto todoQueryParameters);
    }
}
