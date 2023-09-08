using BuySell.Data.Entities;
using BuySell.Data.Enums;
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
    }
}
