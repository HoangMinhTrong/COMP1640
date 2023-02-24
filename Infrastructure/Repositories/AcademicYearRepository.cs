using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AcademicYearRepository : BaseRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AcademicYear?> GetLatestAcademicYearAsync()
        {
            return await GetAllQuery()
                .OrderByDescending(y => y.Id)
                .FirstOrDefaultAsync();
        }
    }
}
