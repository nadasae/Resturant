using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.MenuItems.Requests
{
    public record UpdateMenuItemRequest(int Id ,string Name, decimal Price, int CategoryId);
  
}
