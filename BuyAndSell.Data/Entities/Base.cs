using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Entities
{
    public abstract  class Base
    {
        public long Id { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
        public long CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; } = default!;
        public long UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; } = default!;
    }
}
