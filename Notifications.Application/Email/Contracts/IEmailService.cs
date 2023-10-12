using Notifications.Application.Models.Email;

namespace Notifications.Application.Email.Contracts
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailToSendDto email, ServerCredentialsConfiguration credentialsConfiguration);
    }

}
