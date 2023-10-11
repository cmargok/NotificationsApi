using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Configurations
{
    
    public class OutlookSettings
    {
        public string RemitenteOutlook { get; set; } = string.Empty;
        public string PassWordOutlook { get; set; } = string.Empty;
        public string Url {  get; set; } = string.Empty;

        public string[] GetData() => [RemitenteOutlook, PassWordOutlook];
    }
}
