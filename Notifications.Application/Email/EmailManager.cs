using Microsoft.Extensions.Options;
using Notifications.Application.Azure;
using Notifications.Application.Configurations;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email;
using Notifications.Application.Utils;

namespace Notifications.Application.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailService _emailService;
        private readonly ISecretsManager _secretsManager;
        private readonly CredentialsKeySettings _credentialsKeySettings;
        
        public EmailManager(IEmailFactory emailFactory, IOptions<CredentialsKeySettings> settings, ISecretsManager secretsManager, EnumMailServices mailService)
        {
            _emailService = emailFactory.GetEmailService(mailService); 
            _credentialsKeySettings = settings.Value;
            _secretsManager = secretsManager;
        }


        public async Task<bool> SendEmail(EmailToSendDto emailToSend)
        {
            if(ValidateData(emailToSend))
            {
                var Credentials = await SetOutlookCredentials();

                var response = await _emailService.SendEmail(emailToSend, Credentials);

                return response;               
            }
            return false;
        }

        private bool ValidateData(EmailToSendDto emailToSend)
        {
            ArgumentNullException.ThrowIfNull(emailToSend);

            if (string.IsNullOrEmpty(emailToSend.EmailTo)) return false;

            if (string.IsNullOrEmpty(emailToSend.Subject) && emailToSend.Subject.Length > 2)  return false;

            if (emailToSend.Html && string.IsNullOrEmpty(emailToSend.HtmlBody)) return false;

            if (!emailToSend.Html && string.IsNullOrEmpty(emailToSend.Message))  return false;

            return true;
        }


      
        private async Task<OutlookCredentials> SetOutlookCredentials()
        {
            var secretsDictionary = await _secretsManager.GetSecretsAsync(_credentialsKeySettings.Url, _credentialsKeySettings.GetData());

            var credentials = new OutlookCredentials { Email = secretsDictionary[_credentialsKeySettings.RemitenteOutlook] };

            credentials.SetPassword(secretsDictionary[_credentialsKeySettings.PassWordOutlook]);

            return credentials;
        }

    }

}
