
using Microsoft.Extensions.Options;
using Notifications.Application.Azure;
using Notifications.Application.Configurations;
using Notifications.Application.Email;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email;
using Notifications.Infraestruture.Email;
using Notifications.Infraestruture.Email.Services;
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
            //email manager
            services.AddScoped<IEmailManager, EmailManager>(provider =>
            {
                var instance = new EmailManager(
                    provider.GetRequiredService<IEmailFactory>(), 
                    provider.GetRequiredService<IOptions<CredentialsKeySettings>>(), 
                    provider.GetRequiredService<ISecretsManager>(), Application.Utils.EnumMailServices.MailNet);

                return instance;
            });

            //factory to instance the mailservice library to use
            services.AddScoped<IEmailFactory, EmailFactory>();

            //azure secrets
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
            services.Configure<CredentialsKeySettings>(options =>
            {
                options.Url = Environment.GetEnvironmentVariable("VaultUri")!.ToString();
            });

            services.Configure<CredentialsKeySettings>(config.GetSection("Outlook"));
            return services;
        }
    }
}