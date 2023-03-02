namespace Domain.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<bool> CommentIdea(Comment comment);
    }
}
