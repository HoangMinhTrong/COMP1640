namespace Domain.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetAsync(RoleTypeEnum role);
    }
}
