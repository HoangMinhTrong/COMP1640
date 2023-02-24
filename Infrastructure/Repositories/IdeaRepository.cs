using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class IdeaRepository : BaseRepository<Idea>, IIdeaRepository
    {
        public IdeaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Idea> GetById(int id)
        {
            return GetQuery(_ => _.Id == id);
        }
    }
}
