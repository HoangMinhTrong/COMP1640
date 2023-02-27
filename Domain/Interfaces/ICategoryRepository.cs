namespace Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetAsync(int categoryId);
        IQueryable<Category> GetById(int id);
    }
}
