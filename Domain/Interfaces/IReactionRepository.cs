namespace Domain.Interfaces
{
    public interface IReactionRepository : IBaseRepository<Reaction>
    {
        Task<Reaction> GetByUserAndIdeaAsync(int ideaId, int userId);
    }
}
