using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Staff.Responses
{
    public record GetStaffByIdResponse(string Name, StaffRole Role);
  
}
