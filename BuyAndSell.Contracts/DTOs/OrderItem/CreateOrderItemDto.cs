using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.OrderItem
{
    public class CreateOrderItemDto
    {
        public long ItemId { get; set; }
        public int Amount { get; set; }
    }
}
