namespace Domain.Interfaces
{
    public interface IIdeaRepository : IBaseRepository<Idea>
    {
        Task<Idea> GetAsync(int id);
    }
}
