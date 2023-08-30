using AutoMapper;
using BuySell.Contracts.DTOs.Item;
using BuySell.Data.Entities;
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
            CreateMap<Item, ItemViewDto>();
        }
    }
}
