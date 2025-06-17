using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Entities.Base
{
    public abstract class Entity<Key> where Key : struct
    {
        public Key Id { get; protected set; }
        public Entity()
        {
            Id = default;
        }
    }
    }
