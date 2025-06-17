using Resturant.Core.Entities.Models;
using Resturant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<bool> CanCancelAsync(int orderId);
    }

}
