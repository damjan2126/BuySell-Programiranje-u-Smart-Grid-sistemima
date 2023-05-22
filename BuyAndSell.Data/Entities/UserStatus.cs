using BuyAndSell.Data.Entities;
using BuySell.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Entities
{
    public class UserStatus : Base
    {
        public UserStatusEnum Status { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
