using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Orders.Requests;
using Resturant.BL.Shared;
using Resturant.Core.Enums;

namespace Resturant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddOrderRequest request)
        {
            var result = await _orderServices.AddAsync(request);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderServices.GetByIdAsync(new GetOrderByIdRequest(id));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _orderServices.CancelAsync(new CancelOrderRequest(id));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(OrderStatus status)
        {
            var result = await _orderServices.GetByStatusAsync(new GetAllOrdersByStatusRequest(status));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(OrderType type)
        {
            var result = await _orderServices.GetByTypeAsync(new GetAllOrdersByTypeRequest(type));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderServices.GetAllAsync();
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
        }

        //[HttpPut("status")]
        //public async Task<IActionResult> UpdateStatus(UpdateOrderStatusRequest request)
        //{
        //    var result = await _orderServices.UpdateStatusAsync(request);
        //    return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        //}
    }
}
