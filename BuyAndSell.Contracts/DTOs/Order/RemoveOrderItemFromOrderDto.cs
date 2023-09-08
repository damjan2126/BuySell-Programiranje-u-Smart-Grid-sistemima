using BuySell.Contracts.DTOs.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Order
{
    public class RemoveOrderItemFromOrderDto
    {
        public List<RemoveOrderitemDto> Items { get; set; } = new List<RemoveOrderitemDto>();
    }
}
