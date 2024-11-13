using AutoMapper;
using Core.Entities.IdentityEntities;
using Core.Entities.OrderEntities;

namespace Services.OrderServices.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddressDto, Address>().ReverseMap();
            CreateMap<ShippingAddressDto, ShippingAddressDto>();

            CreateMap<Order, OrderResultDto>()
                    .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                    .ForMember(dest => dest.ShippingPrice, option => option.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dest => dest.ProductItemId, option => option.MapFrom(src => src.ProductItemOrdered.ProductItemId))
                    .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ProductItemOrdered.ProductName))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.ProductItemOrdered.PictureUrl))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderUrlResolver>());
        }
    }
}
