using AutoMapper;
using Infrastructure.BasketRepository;
using Infrastructure.BasketRepository.BasketEntities;
using Services.BasketServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
            => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basketdata = await _basketRepository.GetBasketAsync(basketId);
            if (basketdata == null)
                return new CustomerBasketDto();
            var mappedBasketData = _mapper.Map<CustomerBasketDto>(basketdata);
            return mappedBasketData;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var customerData = _mapper.Map<CustomerBasket>(basket);

            var updatedCustomerData = await _basketRepository.UpdateBasketAsync(customerData);
            var mappedupdatedCustomerData = _mapper.Map<CustomerBasketDto>(updatedCustomerData);

            return mappedupdatedCustomerData;

        }
    }
}
