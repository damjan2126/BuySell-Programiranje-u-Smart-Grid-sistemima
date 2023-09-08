using AutoMapper;
using BuySell.Contracts.DTOs.OrderItem;
using BuySell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.MappingProfiles
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemViewDto>();
        }
    }
}
