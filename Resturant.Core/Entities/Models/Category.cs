using Resturant.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Models
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
