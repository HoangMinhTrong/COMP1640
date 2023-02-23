namespace Domain.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<Department> GetAsync(int departmentId);
    }
}
