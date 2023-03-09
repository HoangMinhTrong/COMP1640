using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class IdeaHistoryRepository : BaseRepository<IdeaHistory>, IIdeaHistoryRepository
    {
        public IdeaHistoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}
