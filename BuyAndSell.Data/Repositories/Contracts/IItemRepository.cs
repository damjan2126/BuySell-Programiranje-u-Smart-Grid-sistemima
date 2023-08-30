using BuySell.Data.Entities;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories.Contracts
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllAsync(Query query, long sellerId);
    }
}
