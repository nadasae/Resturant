using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Order.Requests
{
 public record  AddOrderRequest(
     OrderType type,
     string? DeliveryAddress,
     List<int> orderItemIds,
     int? TableId,
     int? StaffId);
}
