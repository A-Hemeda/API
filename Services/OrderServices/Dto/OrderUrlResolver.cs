using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Microsoft.Extensions.Configuration;
using Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices.Dto
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
                return _configuration["BaseUrl"] + source.ProductItemOrdered.PictureUrl;

            return null;
        }
    }
}
