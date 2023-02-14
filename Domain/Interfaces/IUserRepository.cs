namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IQueryable<User> GetById(int id);

    }
}
