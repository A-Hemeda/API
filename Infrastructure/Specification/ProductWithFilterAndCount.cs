using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class ProductWithFilterAndCount : Specification<Product>
    {
        public ProductWithFilterAndCount(ProductSpecifiction specifiction)
            : base(x =>
                   (string.IsNullOrEmpty(specifiction.Search) || x.Name.ToLower().Trim().Contains(specifiction.Search)) &&
                   (!specifiction.BrandId.HasValue || x.ProductBrandId == specifiction.BrandId) &&
                   (!specifiction.TypeId.HasValue || x.ProductTypeId == specifiction.TypeId)
            )
        { }
    }
}
