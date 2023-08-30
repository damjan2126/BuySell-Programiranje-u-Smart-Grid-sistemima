using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Item
{
    public class ItemListViewDto
    {
        public long Count { get; set; }
        public IEnumerable<ItemViewDto> Items { get; set; } = Enumerable.Empty<ItemViewDto>();
    }
}
