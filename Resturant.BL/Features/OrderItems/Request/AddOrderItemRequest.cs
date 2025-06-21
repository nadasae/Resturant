using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.OrderItems.Request
{
    public record AddOrderItemRequest(int MenuItemId,int Quantity );

}
