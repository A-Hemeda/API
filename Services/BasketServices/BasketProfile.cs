using AutoMapper;
using Infrastructure.BasketRepository.BasketEntities;
using Services.BasketServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BasketServices
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemDto>().ReverseMap();
        }
    }
}
