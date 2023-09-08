using AutoMapper;
using BuySell.Contracts.DTOs.Filters;
using BuySell.Contracts.DTOs.Item;
using BuySell.Data.Entities;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.MappingProfiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<ItemCreateDto, Item>();
            CreateMap<ItemUpdateDto, Item>();
            CreateMap<Item, ItemViewDto>()
                .ForMember(dto => dto.DeliveryFee, opt => opt.MapFrom(it => it.CreatedByUser.DeliveryFee));
            CreateMap<ItemQueryFilterDto, ItemQuery>()
                .ForMember(iq => iq.PageNumber, opt => opt.MapFrom(dto => dto.PageNumber ?? 1))
                .ForMember(iq => iq.PageSize, opt => opt.MapFrom(dto => dto.PageSize ?? 10));
        }
    }
}
