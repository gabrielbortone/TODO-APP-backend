using TODO.Api.Domain.Entities;

namespace TODO.Api.Infra.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetByIdentityUserId(string id);
        Task<bool> Create(User userParams);
        Task<bool> Update(User userParams);
        Task<bool> Delete(string id);
    }
}
