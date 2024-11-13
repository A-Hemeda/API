using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductServices.Dto
{
    public class ProductUrlSolver : IValueResolver<Product, ProductResultDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlSolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) 
                return _configuration["BaseUrl"] + source.PictureUrl;

            return null;
        }
    }
}
