using Notifications.Application.Email;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Infraestruture.Email;

namespace Notifications.API.Configurations
{
    public static class DependencyInjectionExtensions
    {        
       
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<IEmailStrategy, EmailStrategy>();
            return services;
        }
    }    
}