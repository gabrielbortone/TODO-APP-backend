using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Infra.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<CategoryResume> GetByIdAsync(Guid id);
        Task<IEnumerable<CategoryResume>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
