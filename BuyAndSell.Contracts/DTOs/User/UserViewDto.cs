using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Contracts.DTOs.User
{
    public class UserViewDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public ICollection<string> Roles { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
    }
}
