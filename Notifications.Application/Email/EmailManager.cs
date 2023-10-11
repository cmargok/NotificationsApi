using Microsoft.Extensions.Options;
using Notifications.Application.Azure;
using Notifications.Application.Configurations;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;

namespace Notifications.Application.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailOutlook _outlook;
        private readonly ISecretsManager _secretsManager;
        private readonly OutlookSettings _OutlookSettings;

        public EmailManager(IEmailOutlook outlook, IOptions<OutlookSettings> settings, ISecretsManager secretsManager)
        {
            _outlook = outlook;
            _OutlookSettings = settings.Value;
            _secretsManager = secretsManager;
        }


        public async Task<bool> SendEmail(EmailToSendDto emailToSend)
        {
            if(ValidateData(emailToSend))
            {
                var Credentials = await SetOutlookCredentials();

                var response = await _outlook.SendEmail(emailToSend, Credentials);

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
            var secretsDictionary = await _secretsManager.GetSecretsAsync(_OutlookSettings.Url, _OutlookSettings.GetData());


            var credentials = new OutlookCredentials { Email = secretsDictionary[_OutlookSettings.RemitenteOutlook] };

            credentials.SetPassword(secretsDictionary[_OutlookSettings.PassWordOutlook]);

            return credentials;
        }

    }

}
