using BuySell.Contracts.DTOs.OrderItem;
using BuySell.Contracts.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Order
{
    public class OrderViewDto
    {
        public long Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; } 
        public long CreatedByUserId { get; set; }
        public UserViewDto CreatedByUser { get; set; } = default!;
        public long UpdatedByUserId { get; set; }
        public UserViewDto UpdatedByUser { get; set; } = default!;
        public virtual List<OrderItemViewDto> Items { get; set; } = new();
        public int Cost { get; set; }
        public string Status { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Comment { get; set; } = default!;
        public DateTime? DeliveryTime { get; set; }
    }
}
