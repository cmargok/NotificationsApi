using Microsoft.Extensions.Options;
using Notifications.Application.Azure;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email;
using Notifications.Application.Models.Email.Settings;
using Notifications.Application.Utils;

namespace Notifications.Application.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailService _emailService;
        private readonly ISecretsManager _secretsManager;
        private readonly MailSettings _credentialsKeySettings;
        private readonly string _KvUrl;

        public EmailManager(IEmailFactory emailFactory, MailSettings settings, 
                            ISecretsManager secretsManager, EnumMailServices mailService, string kvUrl)
        {
            _emailService = emailFactory.GetEmailService(mailService); 
            _credentialsKeySettings = settings;
            _secretsManager = secretsManager;
            _KvUrl = kvUrl;
        }


        public async Task<bool> SendEmail(EmailToSendDto emailToSend)
        {
            if(ValidateData(emailToSend))
            {
               // var Credentials = await GetCredentials();
                var Credentials = getfromConsole();
                var credentialsConfiguration = new ServerCredentialsConfiguration
                {
                    credentials = Credentials,
                    config = _credentialsKeySettings.ServerConfiguration
                };

                var response = await _emailService.SendEmail(emailToSend, credentialsConfiguration);

                return response;               
            }
            return false;
        }

        private bool ValidateData(EmailToSendDto emailToSend)
        {
            ArgumentNullException.ThrowIfNull(emailToSend);

            if (!emailToSend.EmailsTo.Any() || emailToSend.EmailsTo.Any(to => string.IsNullOrEmpty(to.Email))) return false;

            if (string.IsNullOrEmpty(emailToSend.Subject) && emailToSend.Subject.Length > 2)  return false;

            if (emailToSend.Html && string.IsNullOrEmpty(emailToSend.HtmlBody)) return false;

            if (!emailToSend.Html && string.IsNullOrEmpty(emailToSend.Message))  return false;

            return true;
        }


      
        private async Task<Credentials> GetCredentials()
        {
            var secretsDictionary = await _secretsManager.GetSecretsAsync(_KvUrl, _credentialsKeySettings.GetCredentials());

            var credentials = new Credentials { UserName = secretsDictionary[_credentialsKeySettings.UserName!] };

            credentials.SetPassword(secretsDictionary[_credentialsKeySettings.Password!]);

            return credentials;
        }

        private Credentials getfromConsole() {
            Console.WriteLine("escriba correo");
            string correo = Console.ReadLine();

            Console.WriteLine("escriba la ");

            string pass = Console.ReadLine();

            var credentials = new Credentials { UserName = correo };

            credentials.SetPassword(pass);
            return credentials;


        }

    }

}
