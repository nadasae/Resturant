using Resturant.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Models
{
    public class OrderItem : Entity<int>
    {
        // MenuItems ---> M to M <--- Orders
        // OrderItem is the relationship table
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }

        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
