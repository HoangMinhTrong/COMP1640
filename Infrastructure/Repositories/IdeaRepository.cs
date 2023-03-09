using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IdeaRepository : BaseRepository<Idea>, IIdeaRepository
    {
        public IdeaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Idea> GetAsync(int id)
        {
            return await GetAsync(_ => _.Id == id);
        }

        public IQueryable<Idea> GetById(int id)
        {
            return GetQuery(_ => _.Id == id);
        }

        public async Task<List<Idea>> GetListAsync(int academicYearId)
        {
            return await GetQuery(_ => _.AcademicYearId == academicYearId)
                .OrderByDescending(_ => _.CreatedOn)
                .ToListAsync();
        }
    }
}
