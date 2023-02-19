namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
        IBaseRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
