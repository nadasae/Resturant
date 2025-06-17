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
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<List<Category>> GetWithActiveMenuItemsAsync()
        {
            return await _dbSet
                .Include(c => c.MenuItems)
                .Where(c => c.MenuItems.Any(m => m.IsAvailable))
                .ToListAsync();
        }
    }

}
