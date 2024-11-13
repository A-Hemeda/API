using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.BasketRepository;
using Infrastructure.Interfaces;
using Infrastructure.Specification;
using Microsoft.Extensions.Configuration;
using Services.BasketServices;
using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Core.Entities.Product;

namespace Services.PaymentServices
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices ;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PaymentServices(IUnitOfWork unitOfWork, IBasketServices basketServices, IConfiguration config , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
            _config = config;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
            var basket = await _basketServices.GetBasketAsync(basketId);

            if (basket == null)
                return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) * (long)(shippingPrice * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) * (long)(shippingPrice * 100))
                };
                await service.UpdateAsync( basket.PaymentIntentId ,options);
            }
            await _basketServices.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentspecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            if (order == null)
                return null;
            order.OrderStatus = OrderStatus.PaymentFaild;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Compelete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentspecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            if (order == null)
                return null;
            order.OrderStatus = OrderStatus.PaymentFaild;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Compelete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }
    }
}
