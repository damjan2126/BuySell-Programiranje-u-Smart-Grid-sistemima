using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Resources
{
    public class Query
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; } = default!;
        public bool AsNoTracking { get; set; } = false;
        public bool Intersect { get; set; } = true;
    }
}
