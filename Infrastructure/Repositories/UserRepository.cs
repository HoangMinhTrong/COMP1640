using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await GetQuery(_ => _.Email== email).FirstOrDefaultAsync();
        }

        public IQueryable<User> GetById(int id)
        {
            return GetQuery(_ => _.Id == id);
        }

        public Task<bool> IsUserLockoutEnableAsync(string email)
        {
            return GetQuery(_ => _.Email == email && _.LockoutEnabled).AnyAsync();
        }
    }
}
