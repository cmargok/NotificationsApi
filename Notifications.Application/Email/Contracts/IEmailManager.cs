using Notifications.Application.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Email.Contracts
{
    public interface IEmailManager
    {

        Task<bool> SendEmail(EmailToSendDto emailToSend);
    }

}
