using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);
        public void UpdateUser(User user) => Update(user); 

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges)
            => await FindAll(trackChanges).ToListAsync();

        public async Task<User> GetUserById(Guid userId, bool trackChanges)
            => await FindByCondition(user => user.Id == userId, trackChanges).SingleOrDefaultAsync();

    }
}
