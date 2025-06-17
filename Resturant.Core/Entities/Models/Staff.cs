using Resturant.Core.Entities.Base;
using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Models
{
    public class Staff : Entity<int>
    {
        public string FullName { get; set; }
        public StaffRole Role { get; set; }

        public ICollection<Order> AssignedOrders { get; set; }
    }
}
