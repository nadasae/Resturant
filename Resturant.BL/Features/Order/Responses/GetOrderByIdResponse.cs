using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Order.Responses
{
    public record GetOrderByIdResponse(
        OrderStatus OrderStatus,
        OrderType OrderType,
        string? DeliveryAddress,
        DateTime CreatedAt,
        List<int> OrderItemIds,
        decimal Total,
        int? TableId,
        int? StaffId);
   
}
