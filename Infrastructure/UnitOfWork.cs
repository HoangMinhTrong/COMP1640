using Domain;
using Domain.Base;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Identity.Interfaces;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentUserInfo _currentUserInfo;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context,
            ICurrentUserInfo currentUserInfo)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _currentUserInfo = currentUserInfo;
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                var transResult = await strategy.ExecuteAsync(async () =>
                {
                    using (var trans = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var result = await func.Invoke();
                            await trans.CommitAsync();
                            return result;
                        }
                        catch (Exception)
                        {
                            await trans.RollbackAsync();
                            throw;
                        }
                    }
                });

                return transResult;
            }
            else
                return await func.Invoke();
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
                    case EntityState.Added:
                        OnEntryAdded(entry);
                        break;

                    case EntityState.Modified:
                        OnEntryModified(entry);
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
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
                var entity = (entry.Entity as ITenantEntity);
                entity.TenantId = _currentUserInfo.TenantId;
            }
        }

        private void OnEntryModified(EntityEntry entry)
        {
            if (entry.Entity is not ITenantAuditEntity) return;

            if (entry.Entity is ITenantAuditEntity entity)
            {
                if (!entry.Property(nameof(Idea.Views)).IsModified)
                {
                    entity.UpdatedOn = DateTime.Now;
                }
            }

        }
    }
}