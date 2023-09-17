using BuySell.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Resources
{
    public class OrderQuery : Query
    {
        public long? CreatedByUserId { get; set; }
        public long? UpdatedByUserId { get; set; }
        public DateTime? MinDeliveryTime { get; set; }
        public DateTime? MaxDeliveryTime { get; set; }
        public long? SellerId { get; set; }
    }
}
