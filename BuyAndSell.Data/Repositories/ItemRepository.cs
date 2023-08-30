using BuySell.Data.Entities;
using BuySell.Data.Extensions;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public async Task<IEnumerable<Item>> GetAllAsync(Query query, long sellerId)
        {
            return await Ctx.Items
                .Where(x => x.CreatedByUserId == sellerId)
                .AddAsNoTracking(query)
                .Sort(query)
                .Paginate(query)
                .ToListAsync();
        }


        /*
         public virtual async Task<IEnumerable<TEntityModel>> GetAllAsync(Query query)
    {
        return await Entity
            .FilterBy(query)
            .AddAsNoTracking(query)
            .Sort(query)
            .Paginate(query)
            .ToListAsync();
    }
         */
    }
}
