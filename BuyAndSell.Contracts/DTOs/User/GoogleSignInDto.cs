using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.User
{
    public class GoogleSignInDto
    {
        public string GoogleToken { get; set; } = default!;
    }
}
