using BuyAndSell.Data;
using BuyAndSell.Data.Repositories;
using BuySell.Data.Entities;
using BuySell.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories
{
    public class UserStatusRepository : BaseRepository<UserStatus>, IUserStatusRepository
    {
        public UserStatusRepository(DatabaseContext ctx) : base(ctx)
        {
        }
    }
}
