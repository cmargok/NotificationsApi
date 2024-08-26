using Microsoft.Extensions.Options;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email;
using Notifications.Application.Models.Email.Settings;
using Notifications.Application.Utils.Enums;
using Notifications.Infraestruture.Email.Services;

namespace Notifications.Infraestruture.Email
{
    public class EmailStrategy : IEmailStrategy
    {
        private readonly MailSettings _settings;

        public EmailStrategy(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public IEmailService GetEmailService()
        {
            var serviceConfig = _settings.Services.First(c => c.ServiceName == nameof(_settings.ServiceName));

            var specificSettings = SetSettingsByGivingServiceName(
                serviceConfig.ServerConfiguration.UseSSL,
                serviceConfig.ServerConfiguration.UseStartTls);


            return _settings.ServiceName switch
            {
                EnumMailServices.NetMail => new MailKitService(specificSettings),
                EnumMailServices.MailKit => new NetMailService(specificSettings),
                _ => throw new NotSupportedException()
            };
        }

        private EmailSpeceificSettings SetSettingsByGivingServiceName(bool useSsl, bool usestartTtl)
        {       
            return new EmailSpeceificSettings()
            {
                Host = _settings.SharedConfig.Host,
                Port = _settings.SharedConfig.Port,
                User = _settings.SharedConfig.User,
                Credentials = GetCredentials(),
                UseSSL = useSsl,
                UseStartTls = usestartTtl                
            };
        }


        private Credentials GetCredentials()
        {
            var credentials = new Credentials
            {
                Email = _settings.AuthEmailSettings.Email
            };
            credentials.SetPassword(_settings.AuthEmailSettings.Pass);

            return credentials;
        }
    }
}
