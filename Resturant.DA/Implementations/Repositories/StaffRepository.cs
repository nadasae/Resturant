using Microsoft.EntityFrameworkCore;
using Resturant.Core.Entities.Models;
using Resturant.Core.Enums;
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
    public class StaffRepository : GenericRepository<Staff, int>, IStaffRepository
    {
        public StaffRepository(AppDbContext context) : base(context) { }

        public async Task<List<Staff>> GetByRoleAsync(StaffRole role)
        {
            return await _dbSet.Where(s => s.Role == role).ToListAsync();
        }
    }

}
