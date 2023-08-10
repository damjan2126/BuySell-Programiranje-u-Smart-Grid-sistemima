using AutoMapper;
using BuySell.Contracts.DTOs.UserStatus;
using BuySell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.MappingProfiles
{
    public class UserStatusMappingProfile : Profile
    {
        public UserStatusMappingProfile()
        {
            CreateMap<UserStatus, UserStatusViewDto>();
        }
    }
}
