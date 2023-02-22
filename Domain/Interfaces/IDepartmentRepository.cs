namespace Domain.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<Department> GetAsync(int departmentId);
        Task AssignDepartmentQa(int departmentId, int userId);

    }
}
