using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
