using Microsoft.EntityFrameworkCore;
using Resturant.Core.Entities.Models;
using Resturant.Core.Interfaces.Repositories;
using Resturant.DA.Context;
using Resturant.DA.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DA.Implementations.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem, int>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext context) : base(context) { }

        public async Task<List<MenuItem>> GetAvailableItemsAsync()
        {
            return await _dbSet.Where(m => m.IsAvailable).ToListAsync();
        }

        public async Task<int> GetDailyOrderCountAsync(int itemId)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Where(oi => oi.MenuItemId == itemId && oi.Order.OrderedAt.Date == today)
                .SumAsync(oi => oi.Quantity);
        }
        public async Task<List<MenuItem>> GetAllMenuItemsWithCategoryAsync(int CategoryId)
        {
            var items= await _dbSet.Where(oi=>oi.CategoryId == CategoryId).ToListAsync();
            return items;
        }
    }

}
