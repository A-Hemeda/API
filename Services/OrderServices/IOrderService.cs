using Core.Entities;
using Services.OrderServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrderForUserAsync(string buyerEmail);
        Task<OrderResultDto> GetOrderByIdAsync(int id , string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
    }
}
