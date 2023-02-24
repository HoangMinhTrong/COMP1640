namespace Domain.Interfaces
{
    public interface IIdeaRepository : IBaseRepository<Idea>
    {
        IQueryable<Idea> GetById(int id);
    }
}
