using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class IdeaRepository : BaseRepository<Idea>, IIdeaRepository
    {
        public IdeaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Idea> GetAsync(int id)
        {
            return await GetAsync(_ => _.Id == id);
        }

        public IQueryable<Idea> GetById(int id)
        {
            return GetQuery(_ => _.Id == id);
        }

        public IQueryable<Idea> GetDeleted()
        {
            return GetQuery(_ => _.IsDeleted);
        }
    }
}
