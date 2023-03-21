using Microsoft.Extensions.Options;
using Notifications.Application.Configurations;
using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;

namespace Notifications.Application.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailOutlook _outlook;
        private readonly OutlookSettings _OutlookSettings;
        public EmailManager(IEmailOutlook outlook, IOptions<OutlookSettings> settings)
        {
            _outlook = outlook;
            _OutlookSettings = settings.Value;
        }

        public async Task<bool> SendEmail(EmailToSendDto emailToSend)
        {
            if(ValidateData(emailToSend))
            {
                var remitente = await SetRemitenteData();
                var response = await _outlook.SendEmail(emailToSend, remitente);

                if (response) return true;               
            }
            return false;
        }

        private bool ValidateData(EmailToSendDto emailToSend)
        {
            if(emailToSend == null) throw new ArgumentNullException("email");

            if (string.IsNullOrEmpty(emailToSend.EmailDestinatario)) return false;

            if (string.IsNullOrEmpty(emailToSend.Asunto) && emailToSend.Asunto.Length > 2)  return false;

            if (emailToSend.Html && string.IsNullOrEmpty(emailToSend.HtmlBody)) return false;

            if (string.IsNullOrEmpty(emailToSend.Mensaje))  return false;

            return true;


        }


        private async Task<RemitenteData> SetRemitenteData()       
        {
            return await _outlook.SetRemitenteData(_OutlookSettings.IsDevelopment, Environment.GetEnvironmentVariable("KeyVaultUrl")!.ToString(), _OutlookSettings.FileName, _OutlookSettings.GetData());
        }

        
    }

}
