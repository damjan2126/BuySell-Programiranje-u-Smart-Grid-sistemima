using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Contracts.DTOs.User
{
    public class UserChangePasswordDto
    {
        public string Password { get; set; } = default!;
    }
}
