using AutoMapper;
using SalesSystem.Core.DTOs.Discount;
using SalesSystem.Core.Entities;

namespace SalesSystem.Api.Mappers
{
    public class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<CreateDto, Discount>()
                .ForMember(e => e.CreatedAt, o => o.Ignore())
                 .ForMember(e => e.UpdatedAt, o => o.Ignore());

            CreateMap<UpdateDto, Discount>();
        }
    }
}
