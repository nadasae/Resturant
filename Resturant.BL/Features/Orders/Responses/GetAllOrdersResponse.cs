using Resturant.Core.Enums;


namespace Resturant.BL.Features.Orders.Responses
{
    public record class GetAllOrdersResponse
    {
        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime OrderedAt { get; set; }
        public List<int> OrderItemIds { get; set; } = new();
        public decimal Total { get; set; }
        public int? TableId { get; set; }
        public int StaffId { get; set; }
    }
}
