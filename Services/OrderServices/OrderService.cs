using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.Interfaces;
using Infrastructure.Specification;
using Services.BasketServices;
using Services.OrderServices.Dto;
using Services.PaymentServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IBasketServices _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentServices _paymentServices;

        public OrderService(IBasketServices basketService
                            , IUnitOfWork unitOfWork 
                            , IMapper mapper
                            , IPaymentServices paymentServices)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentServices = paymentServices;
        }

        public async Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto)
        {
            //Get Basket
            var basket = await _basketService.GetBasketAsync(orderDto.BasketId);
            if (basket == null)
                return null;

            var OrderItems = new List<OrderItemDto>();
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(item.Id);
                var ItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(item.Price, item.Quantity, ItemOrdered);
                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
                
                OrderItems.Add(mappedOrderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //To do 

            var specs = new OrderWithPaymentspecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            if(existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentServices.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }


            //Create Order 
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
            var mappedOrderItems = _mapper.Map<List<OrderItem>>(OrderItems);
            var order = new Order(orderDto.BuyerEmail, mappedShippingAddress, deliveryMethod, mappedOrderItems, subTotal , basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.Compelete();
            
            //Delete basket

            await _basketService.DeleteBasketAsync(orderDto.BasketId);

            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrderForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecificationAsync(specs);
            var mappedOrders = _mapper.Map<List<OrderResultDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var specs = new OrderWithSpecification(id ,buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);
            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }
    }
}
