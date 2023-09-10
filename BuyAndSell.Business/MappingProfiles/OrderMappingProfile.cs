using AutoMapper;
using BuySell.Contracts.DTOs.Filters;
using BuySell.Contracts.DTOs.Order;
using BuySell.Data.Entities;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewDto>();

            CreateMap<OrderQueryFilterDto, OrderQuery>()
               .ForMember(oq => oq.PageNumber, opt => opt.MapFrom(dto => dto.PageNumber ?? 1))
               .ForMember(oq => oq.PageSize, opt => opt.MapFrom(dto => dto.PageSize ?? 10));

            CreateMap<OrderUpdateDto, Order>()
                .ForAllMembers(opts => opts.Condition((src, dst, srcMmb) => srcMmb != null));
        }
    }
}
