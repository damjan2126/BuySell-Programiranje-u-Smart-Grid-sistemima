using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new();
        public virtual ICollection<Role> Roles { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;  
    }
}
