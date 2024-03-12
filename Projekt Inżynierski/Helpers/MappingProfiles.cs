using AutoMapper;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Order;

namespace Projekt_Inżynierski.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(m => m.CategoryName, p => p.MapFrom(f => f.Category.Name) )
                .ForMember(m => m.CompanyName, p => p.MapFrom(f => f.Company.Name) )
                .ForMember(m => m.ImageAdress, p => p.MapFrom<UrlResolverForProduct>() );
            CreateMap<CreateProductRequestDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            
            CreateMap<ShippingAddress, ShippingAddressDto>();
            CreateMap<ShippingAddressDto, ShippingAddress>();

            CreateMap<CartItemDto, CartItem>();
            CreateMap<ClientCartDto, ClientCart>();

            CreateMap<ShippingAddressDto, OrderShippingAddress>();

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(m => m.Id, m => m.MapFrom(f => f.ProductOrdered.ProductId) )
                .ForMember(m => m.Name, m => m.MapFrom(f => f.ProductOrdered.ProductName) )
                .ForMember(m => m.ImageUrl, m => m.MapFrom(f => f.ProductOrdered.ImagePath) )
                .ForMember(m => m.ImageUrl, m => m.MapFrom<UrlResolverForOrderItem>() );

            CreateMap<Order,OrderReturnDto>()
                .ForMember(m => m.ShippingCost, m => m.MapFrom(f => f.ShippingMethod.ShippingFee) )
                .ForMember(m => m.ShippingMethod, m => m.MapFrom(f => f.ShippingMethod.Name) )
                .ForMember(m => m.OrderItemDtos, m => m.MapFrom(f => f.OrderItems) );

        }
    }
}