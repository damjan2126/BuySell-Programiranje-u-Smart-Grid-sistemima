using BuySell.Contracts.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.OrderItem
{
    public class OrderItemViewDto
    {
        public long ItemId { get; set; }
        public virtual ItemViewDto Item { get; set; } = default!;
        public int Amount { get; set; }
        public long Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
    }
}
