
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Configurations;
using Notifications.Application.Email;
using Notifications.Application.Email.Contracts;
using Notifications.Infraestruture.Email;

namespace Notifications.API.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<IEmailOutlook, EmailOutlook>();
            return services;
        }
    }

    public static class OptionsConfigs
    {
        public static IServiceCollection AddOptionsConfigs(this IServiceCollection services, IConfiguration config, bool Isdevelop)
        {
            services.Configure<OutlookSettings>(options =>
            {
                options.IsDevelopment = Isdevelop;
            });

            services.Configure<OutlookSettings>(config.GetSection("Outlook"));



            return services;
        }
    }
}