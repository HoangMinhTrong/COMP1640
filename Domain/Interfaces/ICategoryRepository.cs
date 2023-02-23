namespace Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> Add(Category category);
}