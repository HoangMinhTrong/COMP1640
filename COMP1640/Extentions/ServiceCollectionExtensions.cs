using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var t = configuration.GetConnectionString("Local");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Local"));
            });

            return services;
        }
    }
}