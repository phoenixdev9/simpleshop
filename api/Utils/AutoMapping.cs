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
            CreateMap<AddressDTO, Address>();
            CreateMap<Address, AddressDTO>();
            CreateMap<BasketItem, ReturnBasket>()
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl));
            CreateMap<Order, OrderReturnDTO>();
        }
    }
}