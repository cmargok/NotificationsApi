using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Email
{
    public interface IEmailManager
    {
        void ValidateData();

        void SendEmail();
    }

}
