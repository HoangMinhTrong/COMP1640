using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Utilities.Constants;
using Utilities.EmailService;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;
using Utilities.Identity;
using Utilities.StorageService;
using Utilities.StorageService.DTOs;
using Utilities.StorageService.Interfaces;

namespace Utilities
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration configuration)
        {
            var existed = configuration.GetSection(AppSetting.S3Settings).Exists();
            if (existed)
                services.Configure<S3Setting>(configuration.GetSection(AppSetting.S3Settings));

            return services.AddAWSService<IAmazonS3>()
                           .AddSingleton<IAttachmentService, AttachmentService>();
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            return services.AddScoped<IRazorViewRenderer, RazorViewRenderer>()
                           .AddSingleton<IEmailSender, EmailSender>();
        }

        public static IServiceCollection AddCurrentUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

                return httpContext?.CurrentUser();
            });

            return services;
        }

        public static IServiceCollection AddImplementationInterfaces(this IServiceCollection services
            , Type interfaceType
            , Type implementAssemblyType)
        {
            var implementTypes = Assembly.GetAssembly(implementAssemblyType).GetTypes().Where(_ =>
                        _.IsClass
                        && !_.IsAbstract
                        && !_.IsInterface
                        && !_.IsGenericType
                        && _.GetInterface(interfaceType.Name) != null);

            foreach (var implementType in implementTypes)
            {
                var mainInterfaces = implementType
                    .GetInterfaces()
                    .Where(_ => _.GenericTypeArguments.Count() == 0);

                foreach (var mainInterface in mainInterfaces)
                {
                    services.AddScoped(mainInterface, implementType);
                }
            }

            return services;
        }
    }
}
