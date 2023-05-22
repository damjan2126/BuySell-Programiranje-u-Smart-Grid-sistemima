using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Contracts.DTOs.User
{
    public class UserListViewDto
    {
        public long Count { get; set; }
        public IEnumerable<UserViewDto> Users { get; set; } = Enumerable.Empty<UserViewDto>();
    }
}
