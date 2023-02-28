using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
