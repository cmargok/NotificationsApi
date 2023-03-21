using Notifications.Application.Models.Email;

namespace Notifications.Application.Email.Contracts
{
    public interface IEmailOutlook
    {
        public Task<RemitenteData> SetRemitenteData(bool IsDevelopment, string KeyVaultUrlEnviroment, string FileName, params string[] secrets);
        public Task<bool> SendEmail(EmailToSendDto email, RemitenteData remitente);
    }

}
