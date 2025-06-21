using Resturant.Core.Enums;

namespace Resturant.BL.Features.Orders.Requests
{
   public record GetAllOrdersByTypeRequest(OrderType type);
   
}
