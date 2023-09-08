using AutoMapper;
using BuySell.Business.Services.Contracts;
using BuySell.Contracts.DTOs.Order;
using BuySell.Contracts.Exceptions;
using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<Order> AddItem(long userId, AddOrderItemDto dto)
        {
            var activeOrder = await _orderRepository.GetOrderByStatus(OrderStatusEnum.Created, userId);
            var orderItems = _mapper.Map<List<OrderItem>>(dto.Items);
            orderItems.ForEach(x => { x.CreatedByUserId = userId; x.UpdatedByUserId = userId; });

            if (activeOrder is not null)
            {
                activeOrder.Items.AddRange(orderItems);
                await _orderRepository.UpdateAsync(activeOrder);

                /*var items = await _itemRepository.GetAllByIds(activeOrder.Items.Select(x => x.ItemId));
                foreach(var entity in items)
                {
                    entity.Ammount -= activeOrder.Items.FirstOrDefault(x => x.ItemId == entity.Id)!.Amount;
                }*/

                return activeOrder;
            }

            var order = new Order() 
            { CreatedByUserId = userId, 
                UpdatedByUserId = userId, 
                Items = orderItems, 
                Address = String.Empty, 
                Comment = String.Empty 
            };

            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order> GetOrder(long userId)
        {
            return await _orderRepository.GetOrderByStatus(OrderStatusEnum.Created, userId) ??
                throw new NotFoundException("Nije pronadjen otvoren order za ovaj id");
        }

        public async Task<Order> RemoveItem(long userId, RemoveOrderItemFromOrderDto dto)
        {
            var activeOrder = await _orderRepository.GetOrderByStatus(OrderStatusEnum.Created, userId) ??
                throw new NotFoundException("Nije pronadjen otvoren order za ovaj id");

            var ids = dto.Items.Select(x => x.Id);

            activeOrder.Items.RemoveAll(x => ids.Contains(x.Id));

            await _orderRepository.UpdateAsync(activeOrder);

            return activeOrder;
        }
    }
}
