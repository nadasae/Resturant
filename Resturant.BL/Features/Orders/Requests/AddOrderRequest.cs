using Resturant.BL.Features.OrderItems.Request;
using Resturant.Core.Enums;


namespace Resturant.BL.Features.Orders.Requests
{
    public record AddOrderRequest(
        OrderType Type,
        string? DeliveryAddress,
        List<AddOrderItemRequest> OrderItems,
        int? TableId,
        int StaffId);
}

