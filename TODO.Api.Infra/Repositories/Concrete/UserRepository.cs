using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;
using TODO.Api.Infra.Context;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Infra.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoItemDbContext _dbContext;
        public UserRepository(TodoItemDbContext todoItemDbContext)
        {
            _dbContext = todoItemDbContext ?? throw new ArgumentNullException(nameof(todoItemDbContext));
        }

        public async Task<User> GetByIdentityUserId(string id)
        {
            return await _dbContext.ToDoUsers
                .SingleOrDefaultAsync(x => x.IdentityUserId == id);
        }

        public async Task<bool> Update(User userParams)
        {
            var user = await this.GetByIdentityUserId(userParams.IdentityUserId);

            user.Update(userParams.FirstName, userParams.LastName, userParams.PictureUrl);

            _dbContext.ToDoUsers.Update(user);

            return await _dbContext.Commit();
        }

        public async Task<bool> Create(User userParams)
        {
            var user = new User(userParams.IdentityUserId, userParams.FirstName, userParams.LastName,  userParams.PictureUrl);
            await _dbContext.ToDoUsers.AddAsync(user);
            return await _dbContext.Commit();
        }

        public async Task<bool> Delete(string id)
        {
            var user = await this.GetByIdentityUserId(id);
            if (user == null)
                return false;

            user.Delete();

            _dbContext.ToDoUsers.Update(user);

            return await _dbContext.Commit();
        }
    }
}
