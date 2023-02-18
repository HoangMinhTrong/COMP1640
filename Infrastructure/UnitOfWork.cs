using Domain.Base;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Utilities.Identity.Interfaces;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context, ICurrentUserInfo currentUserInfo)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _currentUserInfo = currentUserInfo;
        }

        public virtual IBaseRepository<T> Repository<T>() where T : class
        {
            return _serviceProvider.GetService<IBaseRepository<T>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        OnEntryAdded(entry);
                        break;

                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    case Microsoft.EntityFrameworkCore.EntityState.Detached:
                    case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        break;
                }
            }

            int saved = await _context.SaveChangesAsync();
            return saved;
        }

        private void OnEntryAdded(EntityEntry entry)
        {
            if (typeof(ITenantAuditEntity).IsAssignableFrom(entry.Entity.GetType()))
            {
                var entity = (entry.Entity as ITenantAuditEntity);
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = _currentUserInfo.Id;
                entity.TenantId = _currentUserInfo.TenantId;
            }
            else if (typeof(ITenantEntity).IsAssignableFrom(entry.Entity.GetType()))
            {
                var entity  = (entry.Entity as ITenantEntity);
                entity.TenantId = _currentUserInfo.TenantId;
            }
        }
    }
}
