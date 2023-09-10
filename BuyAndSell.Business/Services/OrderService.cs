using AutoMapper;
using BuySell.Business.Services.Contracts;
using BuySell.Contracts.DTOs.Order;
using BuySell.Contracts.Exceptions;
using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
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

        public async Task<Order> ChangeState(long id, long userId, ChangeOrderStateDto dto)
        {
            var activeOrder = await _orderRepository.GetAsync(x => x.Id == id) ??
                throw new NotFoundException("Ne postoji order sa tim id-jem");

            if (activeOrder.CreatedByUserId != userId) throw new MethodNotAllowedException("Nije moguce menjati tudje ordere");

            switch (dto.State)
            {
                case OrderStatusEnum.InProgress:
                    await ChangeStateToInProgress(userId, activeOrder);
                    break;
                case OrderStatusEnum.Aborted:
                    await ChangeStateToAborted(userId, activeOrder);
                    break;
                default:
                    throw new MethodNotAllowedException($"Nije moguce menjati u stanje {dto.State.ToString()}");
            }

            return activeOrder;
        }

        private async Task ChangeStateToInProgress(long userId, Order order)
        {
            if (order.Status != OrderStatusEnum.Created) throw new MethodNotAllowedException("Nije moguce menjati u ovo stanje");

            if (string.IsNullOrEmpty(order.Address) || string.IsNullOrWhiteSpace(order.Address)) throw new MethodNotAllowedException("Adresa mora biti popunjena pre slanja");

            var items = await _itemRepository.GetAllByIds(order.Items.Select(x => x.ItemId));
            order.Cost = 0;
            foreach(var entity in items)
            {
                var item = order.Items.FirstOrDefault(x => x.ItemId == entity.Id)!;
                entity.Ammount -= item.Amount;
                if (entity.Ammount < 0) throw new MethodNotAllowedException($"Nije moguce izvrsiti porudzbinu, Predmet: {entity.Name} nema dovoljno kolicine");

                await _itemRepository.UpdateAsync(entity);

                order.Cost += entity.Price * item.Amount + entity.CreatedByUser.DeliveryFee ?? 0;
            }

            Random random = new();
            int randomHours = random.Next(1, 25);

            order.DeliveryTime = DateTime.UtcNow.AddHours(randomHours);
            order.UpdatedAtUtc = DateTime.UtcNow;
            order.Status = OrderStatusEnum.InProgress;

            await _orderRepository.UpdateAsync(order);
        }

        private async Task ChangeStateToAborted(long userId, Order order)
        {
            if (order.Status != OrderStatusEnum.InProgress) throw new MethodNotAllowedException("Nije moguce menjati u ovo stanje");

            if (DateTime.UtcNow - order.UpdatedAtUtc > TimeSpan.FromHours(1)) throw new MethodNotAllowedException("Nije moguce otkazati porudzbinu posle sat vremena");

            var items = await _itemRepository.GetAllByIds(order.Items.Select(x => x.ItemId));
            foreach (var entity in items)
            {
                entity.Ammount += order.Items.FirstOrDefault(x => x.ItemId == entity.Id)!.Amount;
                await _itemRepository.UpdateAsync(entity);
            }

            order.UpdatedAtUtc = DateTime.UtcNow;
            order.DeliveryTime = null;
            order.Status = OrderStatusEnum.Aborted;

            await _orderRepository.UpdateAsync(order);

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

        public async Task<OrderListViewDto> GetAllOrders(OrderQuery query, List<int>? status)
        {
            query.AsNoTracking = true;
            var orders = await _orderRepository.GetAllAsync(query, status);
            long count = await _orderRepository.CountAllAsync(query, status);

            var response = new OrderListViewDto()
            {
                Count = count,
                Orders = _mapper.Map<IEnumerable<OrderViewDto>>(orders)
            };

            return response;
        }

        public async Task<Order> UpdateOrder(long id, OrderUpdateDto dto, long userId)
        {
            var activeOrder = await _orderRepository.GetAsync(x => x.Id == id) ??
                throw new NotFoundException("Ne postoji order sa tim id-jem");

            if (activeOrder.CreatedByUserId != userId) throw new MethodNotAllowedException("Nije moguce menjati tudje ordere");

            _mapper.Map(dto, activeOrder);

            await _orderRepository.UpdateAsync(activeOrder);

            return activeOrder;
        }
    }
}
