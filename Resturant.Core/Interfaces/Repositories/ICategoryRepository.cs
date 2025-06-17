using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        Task<List<Category>> GetWithActiveMenuItemsAsync();
      
    }
}
