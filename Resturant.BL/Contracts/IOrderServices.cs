using Resturant.BL.Features.Orders.Requests;
using Resturant.BL.Features.Orders.Responses;
using Resturant.BL.Shared;

namespace Resturant.BL.Contracts
{
    public interface IOrderServices
    {
        Task<Result<AddOrderResponse>> AddAsync(AddOrderRequest request);

        Task<Result<GetOrderByIdResponse>> GetByIdAsync(GetOrderByIdRequest request);

        Task<Result<CancelOrderResponse>> CancelAsync(CancelOrderRequest request);

        Task<Result<List<GetAllOrdersResponse>>> GetByStatusAsync(GetAllOrdersByStatusRequest request);

        Task<Result<List<GetAllOrdersResponse>>> GetByTypeAsync(GetAllOrdersByTypeRequest request);

        Task<Result<List<GetAllOrdersResponse>>> GetAllAsync();

        // Uncomment this if you implement status update logic:
        // Task<Result<UpdateOrderStatusResponse>> UpdateStatusAsync(UpdateOrderStatusRequest request);
    }
}
