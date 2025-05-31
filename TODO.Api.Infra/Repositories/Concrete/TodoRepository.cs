using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Context;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Infra.Repositories.Concrete
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoItemDbContext _dbContext;
        public TodoRepository(TodoItemDbContext todoItemDbContext)
        {
            _dbContext = todoItemDbContext ?? throw new ArgumentNullException(nameof(todoItemDbContext));
        }

        public async Task<ToDoItemResume> AddAsync(TodoItem todoItem, string userId)
        {
            var actualUser = await _dbContext.GetCurrentUserIdAsync(userId);
            todoItem.ConfigureUser(actualUser);

            await _dbContext.TodoItems.AddAsync(todoItem);
            if (await _dbContext.Commit())
            {
                return todoItem.ToResumeObject();
            }

            return null;
        }
        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            var todoItem = await this.GetByIdAsync(id, userId);
            if (todoItem == null || todoItem.User.IdentityUserId != userId)
            {
                return false;
            }

            _dbContext.TodoItems.Remove(todoItem);
            return await _dbContext.Commit();
        }

        public async Task<(List<ToDoItemResume>, int totalPages, int totalItems)> GetAllAsync(
            string userId, 
            int page = 1, 
            int itemsPerPage = 10)
        {
            var todoItems = _dbContext.TodoItems
                .Include(titem => titem.Category)
                .Where(t => t.User.IdentityUserId == userId && t.FinishDate == null)
                .Select(t => t.ToResumeObject());

            var totalItems = await todoItems.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            var result = await todoItems.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();


            return (result, totalPages, totalItems);
        }
        public async Task<(List<ToDoItemResume>, int totalPages, int totalItems)> GetAllAsync(
            string userId, string search = null, string orderBy = "Title", string orderDirection = "asc", 
            int? priority = null, DateTime? dueDate = null, DateTime? finishDate = null, 
            bool? includeCompleted = false, int page = 1, int itemsPerPage = 10)
        {
            var query = _dbContext.TodoItems
                .Include(t => t.Category)
                .Include(t => t.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search) || 
                    t.Description.Contains(search) ||
                    t.Category.Name.Contains(search) ||
                    t.Category.Tags.Contains(search));
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == (Priority)priority.Value);
            }

            if (dueDate.HasValue)
            {
                query = query.Where(t => t.DueDate == dueDate.Value);
            }

            if (finishDate.HasValue)
            {
                query = query.Where(t => t.FinishDate == finishDate.Value);
            }

            if (includeCompleted.HasValue && includeCompleted.Value)
            {
                query = query.Where(t => t.FinishDate != null);
            }
            else
            {
                query = query.Where(t => t.FinishDate == null);
            }

            query = query.Where(t => t.User.IdentityUserId == userId);

            query = orderBy switch
            {
                "Title" => orderDirection.ToLower() == "asc" ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title),
                "Priority" => orderDirection.ToLower() == "asc" ? query.OrderBy(t => t.Priority) : query.OrderByDescending(t => t.Priority),
                "DueDate" => orderDirection.ToLower() == "asc" ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate),
                "FinishDate" => orderDirection.ToLower() == "asc" ? query.OrderBy(t => t.FinishDate) : query.OrderByDescending(t => t.FinishDate),
                _ => query.OrderBy(t => t.Title), 
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            return (await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(t => t.ToResumeObject())
                .ToListAsync(), totalPages, totalItems);
        }
        public async Task<TodoItem> GetByIdAsync(Guid id, string userId)
        {
            return await _dbContext.TodoItems
                .Include(t => t.Category)
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Id == id && x.User.IdentityUserId == userId && x.IsDeleted == false);
        }
        public async Task<ToDoItemResume> MarkAsync(Guid id, string userId)
        {
            var todoItem = await _dbContext.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(x=> x.Id == id);

            if (todoItem == null || todoItem.User.IdentityUserId != userId)
            {
                return null;
            }

            todoItem.Mark();

            _dbContext.TodoItems.Update(todoItem);

            if (await _dbContext.Commit())
            {
                return todoItem.ToResumeObject();
            }

            return null;
        }

        public async Task<bool> UpdateAsync(TodoItem todoItem)
        {
            _dbContext.TodoItems.Update(todoItem);
            
            return await _dbContext.Commit();
        }
    }
}
