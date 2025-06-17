using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Order.Requests
{
   public record GetAllOrdersByTypeRequest(OrderType type);
   
}
