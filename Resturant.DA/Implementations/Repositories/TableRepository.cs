using Microsoft.EntityFrameworkCore;
using Resturant.Core.Entities.Models;
using Resturant.Core.Interfaces.Repositories;
using Resturant.DA.Context;
using Resturant.DA.Implementations.Base;


namespace Resturant.DA.Implementations.Repositories
{
    public class TableRepository : GenericRepository<Table, int>, ITableRepository
    {
        public TableRepository(AppDbContext context) : base(context) { }

        public async Task<List<Table>> GetAvailableTablesAsync()
        {
            return await _dbSet.Where(t => t.IsAvailable).ToListAsync();
        }
        public async Task<Table?> GetTableByNumberAsync(int number)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.TableNumber == number);
        }
    }

}
