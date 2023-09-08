using BuySell.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Entities
{
    public class Order : Base
    {
        public virtual List<OrderItem> Items { get; set; } = new();
        public int Cost { get; set; } = 0;
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Created;
        public string Address { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public DateTime? DeliveryTime { get; set; }


    }
}
