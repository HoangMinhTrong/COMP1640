using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Category> Add(Category category)
    {
        await  DbContext.Categories.AddAsync(category);
        await DbContext.SaveChangesAsync();
        return category;
    }
}