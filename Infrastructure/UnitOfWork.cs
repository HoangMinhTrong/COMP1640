using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public virtual IBaseRepository<T> Repository<T>() where T : class
        {
            return _serviceProvider.GetService<IBaseRepository<T>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            // Custom here
            // ...

            int saved = await _context.SaveChangesAsync();
            return saved;
        }
    }
}
