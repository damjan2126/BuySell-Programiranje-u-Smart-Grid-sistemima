using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuySell.Contracts.DTOs.User
{
    public class UserCreateDto
    {
        public string Firstname { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string Role { get; set; } = default!;
    }
}
