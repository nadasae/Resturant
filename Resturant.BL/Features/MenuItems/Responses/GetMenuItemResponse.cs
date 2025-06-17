using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.MenuItems.Responses
{
   public record GetMenuItemResponse(int Id,string Name ,decimal Price,int CategoryId,bool IsAvailable);
  
}
