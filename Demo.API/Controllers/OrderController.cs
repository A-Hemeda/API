using Core.Entities;
using Demo.API.HandleResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.OrderServices;
using Services.OrderServices.Dto;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);

            if (order == null)
                return BadRequest(new ApiResponse (400, "Error While Creating the order !!"));

            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrderForUserAsync(string buyerEmail)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetAllOrderForUserAsync(email);

            if (order is { Count: <= 0 })
                return Ok(new ApiResponse(200, "this user does not have an order"));

            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id , email);

            if (order is null)
                return Ok(new ApiResponse(200, $"There is no order with this id {id}"));

            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodAsync()
            => Ok(await _orderService.GetAllDeliveryMethodAsync());
    }
}
