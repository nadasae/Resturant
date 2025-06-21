using AutoMapper;
using Resturant.BL.Features.OrderItems.Request;
using Resturant.BL.Features.Orders.Requests;
using Resturant.BL.Features.Orders.Responses;
using Resturant.Core.Entities.Models;
using Resturant.Core.Enums;

namespace Resturant.BL.Features.Orders.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<AddOrderRequest, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(oi => new OrderItem
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity
                })));

            CreateMap<Order, AddOrderResponse>();
          
            CreateMap<Order, GetOrderByIdResponse>()
               .ForMember(dest => dest.OrderItemIds, opt => opt.MapFrom(src => src.OrderItems.Select(oi => oi.Id).ToList()));

            CreateMap<Order, CancelOrderResponse>();

            CreateMap<Order, GetAllOrdersResponse>()
                .ForMember(dest => dest.OrderItemIds, opt => opt.MapFrom(src => src.OrderItems.Select(oi => oi.Id).ToList()));



        }
    }
}
