using TODO.Api.Domain.Entities;
using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Infra.Repositories.Abstract
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetByIdAsync(Guid id, string userId);
        Task<(List<ToDoItemResume>, int totalPages, int totalItems)> GetAllAsync(string userId,
            string search = null, string orderBy = "Title", string orderDirection = "asc",
            int? priority = null, DateTime? dueDate = null, DateTime? finishDate = null,
            bool? includeCompleted = false, int page = 1, int itemsPerPage = 10);
        Task<ToDoItemResume> AddAsync(TodoItem todoItem, string userId);
        Task<ToDoItemResume> MarkAsync(Guid id, string userId);
        Task<bool> UpdateAsync(TodoItem todoItem);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<(int todoCreated, int todoUpdated, int todoCompleted, int todoRemoved)> GetDashboardAsync(string userId);
    }
}
