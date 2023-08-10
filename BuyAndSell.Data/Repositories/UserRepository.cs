using BuySell.Data.Entities;
using BuySell.Data.Extensions;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _ctx;

        public UserRepository(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<User>> GetAllAsync(Query query)
        {
            return await _ctx.Users
                .Include(x => x.Roles)
                .FilterBy(query)
                .Sort(query)
                .OrderBy(x => x.FirstName)
                .Paginate(query)
                .AddAsNoTracking(query)
                .ToListAsync();
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> predicate, bool asNoTracking = false)
        {
            return await _ctx.Users
                .AddAsNoTracking(asNoTracking)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<long> CountAllAsync(Query query)
        {
            return await _ctx.Users
                .FilterBy(query)
                .AddAsNoTracking(query)
                .LongCountAsync();
        }

        public async Task<User?> Get(Expression<Func<User, bool>> predicate, bool asNoTracking = false)
        {
            return await _ctx.Users
                .AddAsNoTracking(asNoTracking)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task RemoveAllTokensAsync(long userId)
        {
            var tokens = await _ctx.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();
            _ctx.RefreshTokens.RemoveRange(tokens);
            await _ctx.SaveChangesAsync();
        }
    }
}
