using Resturant.Core.Entities.Base;
using Resturant.Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Models
{
    public class Order : Entity<int>
    {
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
        public string? DeliveryAddress { get; set; }
        public decimal Total { get;  set; }


        public ICollection<OrderItem> OrderItems { get; set; }

        public int? TableId { get; set; }
        public Table Table { get; set; }

        public int StaffId { get; set; }
        public Staff AssignedStaff { get; set; }
    }
}
