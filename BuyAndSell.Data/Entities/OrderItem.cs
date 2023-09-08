using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Entities
{
    public class OrderItem : Base
    {
        public long ItemId { get; set; }
        public virtual Item Item { get; set; } = default!;
        public int Amount { get; set; }
        public long OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;
    }
}
