using AutoMapper;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Orders.Requests;
using Resturant.BL.Features.Orders.Responses;
using Resturant.BL.Shared;
using Resturant.Core.Entities.Models;
using Resturant.Core.Enums;
using Resturant.Core.Interfaces.Repositories;

namespace Resturant.BL.AppServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository Orders;
        private readonly IMenuItemRepository MenuItems;
        private readonly ITableRepository Tables;
        private readonly IMapper _mapper;

        public OrderServices(
            IOrderRepository orders,
            IMenuItemRepository menuItems,
            ITableRepository tables,
            IMapper mapper)
        {
            Orders = orders;
            MenuItems = menuItems;
            Tables = tables;
            _mapper = mapper;
        }
        public async Task<Result<AddOrderResponse>> AddAsync(AddOrderRequest request)
        {
            if (request.Type == OrderType.Delivery && string.IsNullOrWhiteSpace(request.DeliveryAddress))
                return Result<AddOrderResponse>.Fail("Delivery address is required for delivery orders.");

            //  Check table availability for DineIn orders
            if (request.Type == OrderType.DineIn)
            {
                if (request.TableId == null)
                    return Result<AddOrderResponse>.Fail("Table is required for dine-in orders.");

                var table = await Tables.GetByIdAsync(request.TableId.Value);
                if (table == null)
                    return Result<AddOrderResponse>.Fail("Table not found.");

                if (!table.IsAvailable)
                    return Result<AddOrderResponse>.Fail("Selected table is currently reserved or unavailable.");
            }

            var menuItems = await Task.WhenAll(
                request.OrderItems.Select(async oi => await MenuItems.GetByIdAsync(oi.MenuItemId))
            );

            if (menuItems.Any(m => m == null))
                return Result<AddOrderResponse>.Fail("Some menu items not found.");

            if (menuItems.Any(m => !m!.IsAvailable))
                return Result<AddOrderResponse>.Fail("Order contains unavailable items.");

            var order = _mapper.Map<Order>(request);
            order.Status = OrderStatus.Pending;
            order.OrderedAt = DateTime.Now;

            order.OrderItems = request.OrderItems.Select(oi =>
            {
                var item = menuItems.First(m => m!.Id == oi.MenuItemId)!;
                return new OrderItem
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    Subtotal = item.Price * oi.Quantity
                };
            }).ToList();

            order.Total = CalculateTotal(order.OrderItems, order.OrderedAt);

            await Orders.AddAsync(order);
            await Orders.SaveChangesAsync();

            foreach (var item in menuItems.DistinctBy(i => i!.Id))
            {
                var count = await MenuItems.GetDailyOrderCountAsync(item!.Id);
                if (count >= 50 && item.IsAvailable)
                {
                    item.IsAvailable = false;
                    MenuItems.Update(item);
                }
            }

            await MenuItems.SaveChangesAsync();

            //  Mark the table as unavailable (reserved) for DineIn
            if (request.Type == OrderType.DineIn)
            {
                var table = await Tables.GetByIdAsync(request.TableId.Value);
                if (table != null)
                {
                    table.IsAvailable = false;
                    Tables.Update(table);
                    await Tables.SaveChangesAsync();
                }
            }

            var mapped = _mapper.Map<AddOrderResponse>(order);
            return Result<AddOrderResponse>.Success(mapped);
        }











        //public async Task<Result<UpdateOrderStatusResponse>> UpdateStatusAsync(UpdateOrderStatusRequest request)
        //{
        //    var order = await Orders.GetByIdAsync(request.OrderId);
        //    if (order == null)
        //        return Result<UpdateOrderStatusResponse>.Fail("Order not found.");

        //    if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Delivered)
        //        return Result<UpdateOrderStatusResponse>.Fail("Cannot cancel or update status for this order.");

        //    order.Status = request.NewStatus;
        //    await Orders.SaveChangesAsync();

        //    var mapped = _mapper.Map<UpdateOrderStatusResponse>(order);
        //    return Result<UpdateOrderStatusResponse>.Success(mapped);
        //}

        public async Task<Result<GetOrderByIdResponse>> GetByIdAsync(GetOrderByIdRequest request)
        {
            var order = await Orders.GetByIdAsync(request.Id);
            if (order == null)
                return Result<GetOrderByIdResponse>.Fail("Order not found.");

            var mapped = _mapper.Map<GetOrderByIdResponse>(order);
            return Result<GetOrderByIdResponse>.Success(mapped);
        }

        public async Task<Result<CancelOrderResponse>> CancelAsync(CancelOrderRequest request)
        {
            var order = await Orders.GetByIdAsync(request.Id);
            if (order == null)
                return Result<CancelOrderResponse>.Fail("Order not found.");

            if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Delivered)
                return Result<CancelOrderResponse>.Fail("Cannot delete a ready or delivered order.");

            Orders.Delete(order);
            await Orders.SaveChangesAsync();
            var mapped = _mapper.Map<CancelOrderResponse>(order);
            return Result<CancelOrderResponse>.Success(mapped);
        }


        public async Task<Result<List<GetAllOrdersResponse>>> GetByStatusAsync(GetAllOrdersByStatusRequest request)
        {
            var orders = Orders.GetAllAsync();
            var filtered = orders.Where(o => o.Status == request.OrderStatus).ToList();

            if (!filtered.Any())
                return Result<List<GetAllOrdersResponse>>.Fail("No orders with this status.");

            var mapped = _mapper.Map<List<GetAllOrdersResponse>>(filtered);
            return Result<List<GetAllOrdersResponse>>.Success(mapped);
        }

        private decimal CalculateTotal(ICollection<OrderItem> items, DateTime orderDate)
        {
            decimal subtotal = items.Sum(i => i.Subtotal);

            if (orderDate.Hour >= 15 && orderDate.Hour < 17)
                subtotal *= 0.8m; // Happy hour 20% off

            if (subtotal > 100)
                subtotal *= 0.9m; // Bulk 10% off

            return subtotal + subtotal * 0.085m; // Add 8.5% tax
        }


        public async Task<Result<List<GetAllOrdersResponse>>> GetByTypeAsync(GetAllOrdersByTypeRequest request)
        {
            var orders = Orders.GetAllAsync();
            var filtered = orders.Where(o => o.Type == request.type).ToList();

            if (!filtered.Any())
                return Result<List<GetAllOrdersResponse>>.Fail("No orders found with this type.");

            var mapped = _mapper.Map<List<GetAllOrdersResponse>>(filtered);
            return Result<List<GetAllOrdersResponse>>.Success(mapped);
        }

        public async Task<Result<List<GetAllOrdersResponse>>> GetAllAsync()
        {
            var orders = Orders.GetAllAsync();
            if (orders == null || !orders.Any())
                return Result<List<GetAllOrdersResponse>>.Fail("No orders found.");

            var mapped = _mapper.Map<List<GetAllOrdersResponse>>(orders.ToList());
            return Result<List<GetAllOrdersResponse>>.Success(mapped);
        }
    }
}
