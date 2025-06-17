using Resturant.Core.Entities.Models;
using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Interfaces.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff, int>
    {
        Task<List<Staff>> GetByRoleAsync(StaffRole role);
    }
}
