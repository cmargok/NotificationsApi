using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Configurations
{
    public class Options
    {
    }

    public class OutlookSettings
    {
        public string RemitenteOutlook { get; set; } = string.Empty;
        public string PassWordOutlook { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public bool IsDevelopment { get; set; } 


        public string[] GetData()
        {
            return new string[] { RemitenteOutlook, PassWordOutlook };
        }
    }
}
