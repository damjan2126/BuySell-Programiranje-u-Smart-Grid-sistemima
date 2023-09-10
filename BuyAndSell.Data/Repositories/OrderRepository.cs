using BuySell.Data.Entities;
using BuySell.Data.Enums;
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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public async Task<Order?> GetOrderByStatus(OrderStatusEnum status, long userId)
        {
            return await Ctx.Orders
                .Include(x => x.CreatedByUser)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.CreatedByUser)
                .FirstOrDefaultAsync(x => x.Status == status && x.CreatedByUserId == userId);
        }

        public override async Task<Order?> GetAsync(Expression<Func<Order, bool>> predicate, bool asNoTracking = false)
        {
            return await Ctx.Orders
                .Include(x => x.CreatedByUser)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.CreatedByUser)
                .AddAsNoTracking(asNoTracking)
                .FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(Query query)
        {
            return await Ctx.Orders
                .Include(x => x.CreatedByUser)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.CreatedByUser)
                .FilterBy(query)
                .AddAsNoTracking(query)
                .Sort(query)
                .Paginate(query)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(OrderQuery query, List<int>? status)
        {
            return await Ctx.Orders
                .Include(x => x.CreatedByUser)
                .Include(x => x.Items)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.CreatedByUser)
                .FilterBy(query)
                .FilterByStatus(status)
                .AddAsNoTracking(query)
                .Sort(query)
                .Paginate(query)
                .ToListAsync();
        }

        public async Task<long> CountAllAsync(OrderQuery query, List<int>? status)
        {
            return await Ctx.Orders
                .FilterBy(query)
                .FilterByStatus(status)
                .AddAsNoTracking(query)
                .LongCountAsync();
        }
    }
}
