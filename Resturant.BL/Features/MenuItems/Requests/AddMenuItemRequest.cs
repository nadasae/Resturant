using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.MenuItems.Requests
{
    public record AddMenuItemRequest(string Name,decimal Price,int CategoryId);

}
