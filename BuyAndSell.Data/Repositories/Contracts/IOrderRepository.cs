using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order?> GetOrderByStatus(OrderStatusEnum status, long userId);
        Task<IEnumerable<Order>> GetAllAsync(OrderQuery query, List<int>? status);
        Task<long> CountAllAsync(OrderQuery query, List<int>? status);
    }
}
