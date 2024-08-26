using Notifications.Application.Models.Email.Settings;
using Notifications.Application.Utils.Enums;

namespace Notifications.Api.Configurations.Modules
{
    public static class EmailExtensions
    {
        private const EnumMailServices MailService = EnumMailServices.NetMail;
        public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddOptions<MailSettings>()
                .BindConfiguration("MailSettings")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.PostConfigure<MailSettings>(c =>
            {
                c.ServiceName = MailService;
                c.AuthEmailSettings = new AuthEmailSettings()
                {
                    Email = Environment.GetEnvironmentVariable("mail")!,
                    Pass = Environment.GetEnvironmentVariable("assp")!,
                };
            });

            return services;
        }
        
    }
}
