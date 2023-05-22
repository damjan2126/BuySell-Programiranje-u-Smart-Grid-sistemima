using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Entities
{
    public class Role : IdentityRole<long>
    {
        public ICollection<User> Users { get; set; } = default!;
    }
}
