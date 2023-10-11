
using Notifications.Application.Azure;
using Notifications.Application.Configurations;
using Notifications.Application.Email;
using Notifications.Application.Email.Contracts;
using Notifications.Infraestruture.Email;
using Notifications.Infraestruture.Externals.Azure;

namespace Notifications.API.Configurations
{/// <summary>
/// 
/// </summary>
    public static class DependencyInjectionExtensions
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<IEmailOutlook, EmailOutlook>();
            services.AddScoped<ISecretsManager, SecretsManager>();
            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class OptionsConfigs
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionsConfigs(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<OutlookSettings>(options =>
            {
                options.Url = Environment.GetEnvironmentVariable("VaultUri")!.ToString();
            });

            services.Configure<OutlookSettings>(config.GetSection("Outlook"));
            return services;
        }
    }
}