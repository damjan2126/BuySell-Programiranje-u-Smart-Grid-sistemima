using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Resources
{
    public class OrderItemQuery : Query
    {
        public long? CreatedByUserId { get; set; }
        public long? UpdatedByUserId { get; set; }
    }
}
