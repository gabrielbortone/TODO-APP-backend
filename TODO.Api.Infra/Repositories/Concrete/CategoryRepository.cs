using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Context;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Infra.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TodoItemDbContext _dbContext;

        public CategoryRepository(TodoItemDbContext todoItemDbContext)
        {
            _dbContext = todoItemDbContext ?? throw new ArgumentNullException(nameof(todoItemDbContext));
        }

        public async Task<IEnumerable<CategoryResume>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Where(x=> x.IsDeleted == false)
                .Select(x => new CategoryResume(x.Id, x.Name, x.Description, x.Tags))
                .ToListAsync();
        }

        public async Task<CategoryResume> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories
                .AsNoTracking()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => new CategoryResume(x.Id, x.Name, x.Description, x.Tags))
                .FirstOrDefaultAsync();
        }
    }
}
