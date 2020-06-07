using api.DTO;
using AutoMapper;
using Domains.Entities;

namespace api.Utils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ReturnProductDTO>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));
        }
    }
}