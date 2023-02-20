using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> GetAsync(RoleTypeEnum role)
        {
            return await GetAsync(_ => _.Id == (int)role);
        }
    }
}
