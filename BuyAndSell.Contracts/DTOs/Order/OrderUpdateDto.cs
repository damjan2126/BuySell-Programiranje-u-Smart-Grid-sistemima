using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Order
{
    public class OrderUpdateDto
    {
        public string? Address { get; set; }
        public string? Comment { get; set; }
    }
}
