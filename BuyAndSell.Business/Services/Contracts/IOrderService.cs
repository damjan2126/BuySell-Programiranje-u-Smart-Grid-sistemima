using BuySell.Contracts.DTOs.Order;
using BuySell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> AddItem(long userId, AddOrderItemDto dto);
        Task<Order> RemoveItem(long userId, RemoveOrderItemFromOrderDto dto);
        Task<Order> GetOrder(long userId);
    }
}
