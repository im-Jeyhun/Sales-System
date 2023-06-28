using AutoMapper;
using SalesSystem.Core.DTOs.Product;
using SalesSystem.Core.Entities;

namespace SalesSystem.Api.Mappers
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<CreateDto, Product>()
                .ForMember(e => e.CreatedAt, o => o.Ignore())
                .ForMember(e => e.UpdatedAt, o => o.Ignore());
            CreateMap<UpdateDto, Product>()
                .ForMember(e => e.CreatedAt, o => o.Ignore())
                .ForMember(e => e.UpdatedAt, o => o.Ignore());
        }
    }
}
