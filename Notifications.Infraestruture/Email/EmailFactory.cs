using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Utils;
using Notifications.Infraestruture.Email.Services;

namespace Notifications.Infraestruture.Email
{
    public class EmailFactory : IEmailFactory
    {
        public IEmailService GetEmailService(EnumMailServices mailServices)
        {

            return mailServices switch
            {
                EnumMailServices.NetMail => new MailKitService(),
                EnumMailServices.MailKit => new MimeKitService(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
