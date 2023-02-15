using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<User> GetById(int id)
        {
            return GetQuery(_ => _.Id == id);
        }
    }
}
