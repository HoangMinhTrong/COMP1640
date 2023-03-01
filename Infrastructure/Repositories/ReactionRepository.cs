using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Reaction> GetByUserAndIdeaAsync(int ideaId, int userId)
        {
            return 
                await GetQuery(_ => _.IdeaId == ideaId && _.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
