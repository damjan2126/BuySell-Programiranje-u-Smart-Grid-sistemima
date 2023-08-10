using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Settings
{
    public class EmailConfigurationSettings
    {
        public string Host { get; set; } = default!;
        public int EmailJobInterval { get; set; }
    }
}
