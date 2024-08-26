﻿using Microsoft.Extensions.Options;
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
      //  private readonly ISecretsManager _secretsManager;
        private readonly MailSettings _credentialsKeySettings;

        public EmailManager(IEmailFactory emailFactory, MailSettings settings, 
                           /* ISecretsManager secretsManager,*/ EnumMailServices mailService)
        {
            _emailService = emailFactory.GetEmailService(mailService); 
            _credentialsKeySettings = settings;
          //  _secretsManager = secretsManager;
        }


        public async Task<bool> SendEmail(EmailToSendDto emailToSend)
        {
            if(ValidateData(emailToSend))
            {
               // var Credentials = await GetCredentials();
                var Credentials = getFromEnviromentVariables();
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

        private static bool ValidateData(EmailToSendDto emailToSend)
        {
            ArgumentNullException.ThrowIfNull(emailToSend);

            if (emailToSend.EmailsTo.Count <= 0 || emailToSend.EmailsTo.Any(to => string.IsNullOrEmpty(to.Email))) 
                return false;

            if (string.IsNullOrEmpty(emailToSend.Subject) && emailToSend.Subject.Length > 2)  
                return false;

            if (emailToSend.Html && string.IsNullOrEmpty(emailToSend.HtmlBody)) 
                return false;

            if (!emailToSend.Html && string.IsNullOrEmpty(emailToSend.Message))  
                return false;

            return true;
        }


      
       /* private async Task<Credentials> GetCredentials()
        {
            var secretsDictionary = await _secretsManager.GetSecretsAsync(_KvUrl, _credentialsKeySettings.GetCredentials());

            var credentials = new Credentials { UserName = secretsDictionary[_credentialsKeySettings.UserName!] };

            credentials.SetPassword(secretsDictionary[_credentialsKeySettings.Password!]);

            return credentials;
        }*/

        private Credentials getFromEnviromentVariables() {

            string correo = Environment.GetEnvironmentVariable("mail")!;

            string pass = Environment.GetEnvironmentVariable("assp")!;

            var credentials = new Credentials { UserName = correo };
            credentials.SetPassword(pass);

            return credentials;
        }

    }

}
