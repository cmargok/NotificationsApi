using Notifications.Application.Utils.Enums;

namespace Notifications.Application.Email.Contracts.Factory
{
    public interface IEmailStrategy
    {

        public IEmailService GetEmailService();
    }


}
