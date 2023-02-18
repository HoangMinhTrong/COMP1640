using COMP1640.Services;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Utilities;

namespace COMP1640.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable("DatabaseConnectionString") ?? configuration.GetConnectionString("Localhost"));
                options.UseLazyLoadingProxies();
            });

            return services;
        }

        public static IServiceCollection AddRepositoriesBase(this IServiceCollection services)
        {
            return
                services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                        .AddImplementationInterfaces(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return
                services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return 
                services.AddScoped<HRMService>();
        }
    }
}