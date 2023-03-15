namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
        IQueryable<User> GetById(int id);
        Task<bool> IsUserLockoutEnableAsync(string email);
    }
}
