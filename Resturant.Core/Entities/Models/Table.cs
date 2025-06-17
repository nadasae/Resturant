using Resturant.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Models
{
    public class Table : Entity<int>
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Order> Orders { get; set; }
    }
}
