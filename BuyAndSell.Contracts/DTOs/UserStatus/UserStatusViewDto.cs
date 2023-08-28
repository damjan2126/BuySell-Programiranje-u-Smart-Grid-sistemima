using BuySell.Contracts.DTOs.User;
using BuySell.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.UserStatus
{
    public class UserStatusViewDto
    {
        public UserStatusEnum Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public UserViewDto User { get; set; } = default!;
    }
}
