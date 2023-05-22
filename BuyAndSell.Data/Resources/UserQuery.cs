using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Resources
{
    public class UserQuery : Query
    {
        public DateTime? MinLastLoginDate { get; set; }
        public DateTime? MaxLastLoginDate { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> Roles { get; set; } = default!;
    }
}
