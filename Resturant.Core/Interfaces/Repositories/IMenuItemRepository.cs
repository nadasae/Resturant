using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Interfaces.Repositories
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem, int>
    {
        Task<List<MenuItem>> GetAvailableItemsAsync();
        Task<int> GetDailyOrderCountAsync(int itemId);
        public Task<List<MenuItem>> GetAllMenuItemsWithCategoryAsync(int CategoryId);

    }
}
