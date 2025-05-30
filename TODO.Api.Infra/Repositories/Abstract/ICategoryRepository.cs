using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Infra.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryResume>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
