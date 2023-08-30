using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.Filters
{
    public class ItemQueryFilterDto : GetAllBaseQueryFilterDto
    {
        public long? CreatedByUserId { get; set; }
        public long? UpdatedByUserId { get; set; }
        public string? Name { get; set; }
    }
}
