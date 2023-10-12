using Notifications.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Models.Email.Settings
{

    public class CredentialsKeySettings
    {
        public List<MailSettings> Services { get; set; }
        public string KvUrl { get; set; } = string.Empty;

        public CredentialsKeySettings()
        {
            Services = new List<MailSettings>();
        }




    }
}
