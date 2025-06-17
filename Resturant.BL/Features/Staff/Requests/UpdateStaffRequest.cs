using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Staff.Requests
{
   public record UpdateStaffRequest(int Id , string Name , StaffRole Role);
  
}
