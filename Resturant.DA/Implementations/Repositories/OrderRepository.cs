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
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var items= await _dbSet.Where(o => o.Status == status).ToListAsync();
            return items;
        }

        public async Task<bool> CanCancelAsync(int orderId)
        {
            var order = await GetByIdAsync(orderId);
            return order is not null && order.Status is not (OrderStatus.Ready or OrderStatus.Delivered);
        }
        public async Task<List<Order>> GetOrdersByTypeAsync(OrderType type)
        {
            var items = await _dbSet.Where(o=>o.Type == type).ToListAsync();
            return items;
        }
    }

}
