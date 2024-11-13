using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public interface IPaymentServices
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId);
        Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
