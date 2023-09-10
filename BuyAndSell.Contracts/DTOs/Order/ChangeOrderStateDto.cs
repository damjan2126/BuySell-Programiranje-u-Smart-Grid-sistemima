using BuySell.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Order
{
    public class ChangeOrderStateDto
    {
        public OrderStatusEnum State { get; set; }
    }
}
