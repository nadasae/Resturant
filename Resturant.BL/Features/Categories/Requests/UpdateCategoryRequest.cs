using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Categories.Requests
{
   public record UpdateCategoryRequest(int Id ,string Name,ICollection<int> menuItemIds);
}
