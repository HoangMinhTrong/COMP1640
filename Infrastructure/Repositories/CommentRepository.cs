using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CommentIdea(Comment comment)
        {
            await DbContext.Comments.AddAsync(comment);
            return true;
        }
    }
}
