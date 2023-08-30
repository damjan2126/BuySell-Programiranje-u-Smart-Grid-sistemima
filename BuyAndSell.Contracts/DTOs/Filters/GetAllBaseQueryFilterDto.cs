using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Filters
{
    public class GetAllBaseQueryFilterDto
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public bool Intersect { get; set; } = false;
    }
}
