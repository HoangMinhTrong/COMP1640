using COMP1640.Services;
using Domain;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Utilities;
using Utilities.Constants;
using Utilities.EmailService.DTOs;

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
            services.AddScoped<HRMService>()
                .AddScoped<AcademicYearService>()
                .AddScoped<IdeaService>()
                .AddScoped<CategoryService>()
                .AddScoped<PersonalService>()
                .AddScoped<AttachmentService>()
                .AddScoped<ReactionService>();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                    .AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedEmail = false)
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                   .AddCookie()
                   .AddOpenIdConnect(options =>
                   {
                       options.SignInScheme = "Cookies";
                       options.Authority = "-your-identity-provider-";
                       options.RequireHttpsMetadata = false;
                       options.ClientId = "-your-clientid-";
                       options.ClientSecret = "-your-client-secret-from-user-secrets-or-keyvault";
                       options.ResponseType = "code";
                       options.UsePkce = true;
                       options.Scope.Add("profile");
                       options.SaveTokens = true;
                   });

            return services;
        }

        public static IServiceCollection AddMailgun(this IServiceCollection services, IConfiguration configuration)
        {
            var existed = configuration.GetSection(AppSetting.MailgunSettings).Exists();
            if (existed)
                services.Configure<MailgunSetting>(configuration.GetSection(AppSetting.MailgunSettings));

            return services;
        }

        public static IServiceCollection AddMailkit(this IServiceCollection services, IConfiguration configuration)
        {
            var existed = configuration.GetSection(AppSetting.MailkitSettings).Exists();
            if (existed)
                services.Configure<MailkitSetting>(configuration.GetSection(AppSetting.MailkitSettings));

            return services;
        }
    }
}