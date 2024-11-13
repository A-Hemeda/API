using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductServices.Dto
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                    .ForMember(dest => dest.ProductTypeName, options => options.MapFrom(src => src.ProductType.Name))
                    .ForMember(dest => dest.ProductBrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                    .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductUrlSolver>());
        }
    }
}
