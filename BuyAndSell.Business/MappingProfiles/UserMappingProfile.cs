using AutoMapper;
using BuySell.Contracts.DTOs.User;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Data.Entities;

namespace BuySell.Business.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(x => x.Name).ToList()));

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
