namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
