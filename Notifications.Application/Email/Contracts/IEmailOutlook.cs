using Notifications.Application.Models.Email;

namespace Notifications.Application.Email.Contracts
{
    public interface IEmailOutlook
    {
        public Task<bool> SendEmail(EmailToSendDto email, OutlookCredentials remitente);
    }

}
