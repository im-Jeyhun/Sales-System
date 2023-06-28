using AutoMapper;
using SalesSystem.Core.DTOs.User;
using SalesSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Api.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateDto, User>()
                .ForMember(e => e.Id , o => o.Ignore())
                .ForMember(e => e.CreatedAt , o => o.Ignore())
                .ForMember(e => e.UpdatedAt , o => o.Ignore());

            CreateMap<User, CreateDto>();
            CreateMap<UpdateDto, User>()
                .ForMember(e => e.Id, o => o.Ignore())
                .ForMember(e => e.CreatedAt, o => o.Ignore())
                .ForMember(e => e.UpdatedAt, o => o.Ignore());
        }
    }
}
