using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Item
{
    public class ItemCreateDto
    {
        public string Name { get; set; } = default!;
        public int Price { get; set; }
        public int Ammount { get; set; }
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
