using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetAsync(int categoryId)
        {
            return await GetAsync(_ => _.Id == categoryId);
        }
        public async Task<Category> Add(Category category)
        {
            await  DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
    }
}
