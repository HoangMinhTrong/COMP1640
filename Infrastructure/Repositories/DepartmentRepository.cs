using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Department> GetAsync(int departmentId)
        {
            return await GetAsync(_ => _.Id == departmentId);
        }

        public IQueryable<Department> GetId(int id)
        {
            return GetQuery(_ => _.Id == id);
        }
    }
}
