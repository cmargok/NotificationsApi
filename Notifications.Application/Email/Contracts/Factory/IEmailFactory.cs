using Notifications.Application.Email.Contracts;
using Notifications.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Email.Contracts.Factory
{
    public interface IEmailFactory
    {

        public IEmailService GetEmailService(EnumMailServices mailServices);
    }


}
