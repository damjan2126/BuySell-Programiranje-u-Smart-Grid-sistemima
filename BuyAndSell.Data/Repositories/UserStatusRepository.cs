using BuySell.Data.Entities;
using BuySell.Data.Extensions;
using BuySell.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories
{
    public class UserStatusRepository : BaseRepository<UserStatus>, IUserStatusRepository
    {
        public UserStatusRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public async Task<IEnumerable<UserStatus>> GetAllAsync(Expression<Func<UserStatus, bool>> predicate, bool asNoTracking = false)
        {
            return await Ctx.UserStatus
                .Include(x => x.User)
                .Where(predicate)
                .AddAsNoTracking(asNoTracking)
                .ToListAsync();
        }

        public async Task<UserStatus?> GetCurrentStatus (long userId)
        {
            return await Ctx.UserStatus.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAtUtc).FirstOrDefaultAsync();
        }
    }
}
