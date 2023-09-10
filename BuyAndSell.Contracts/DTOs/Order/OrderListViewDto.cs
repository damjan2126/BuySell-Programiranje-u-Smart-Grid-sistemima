using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Order
{
    public class OrderListViewDto
    {
        public long Count { get; set; }
        public IEnumerable<OrderViewDto> Orders { get; set; } = Enumerable.Empty<OrderViewDto>();
    }
}
