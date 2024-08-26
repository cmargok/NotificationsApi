using Notifications.Application.Models.Email;
using Notifications.Application.Utils;

namespace Notifications.Application.Email.Contracts
{
    public interface IEmailManager
    {

        Task<ApiResponse<bool>> SendEmailAsync(EmailToSendDto emailToSend);
    }

}
