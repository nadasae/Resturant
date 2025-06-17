using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Tables.Responses
{
   public record GetTableByNumberResponse(int Number, int Capacity, List<int>? OrdersIds);

}
