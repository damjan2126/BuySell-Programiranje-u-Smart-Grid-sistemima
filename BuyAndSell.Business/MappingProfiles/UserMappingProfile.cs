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
            .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(x => x.Name).ToList()))
            .ForMember(x => x.IsActive, opt => opt.MapFrom(y => GetIsActive(y)));

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }

        private static bool GetIsActive(User user)
        {
            if(user.Roles.Select(x => x.Name).Contains("Buyer") || user.Roles.Select(x => x.Name).Contains("Admin"))
            {
                return true;
            }
            return user.Roles.Select(x => x.Name).Contains("Active") ? true : false;
        }
    }
}
