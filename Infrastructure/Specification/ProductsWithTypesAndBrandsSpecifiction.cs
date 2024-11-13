using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class ProductsWithTypesAndBrandsSpecifiction : Specification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifiction(ProductSpecifiction specifiction) 
            : base(x =>
                   (string.IsNullOrEmpty(specifiction.Search) || x.Name.ToLower().Trim().Contains(specifiction.Search))&&
                   (!specifiction.BrandId.HasValue || x.ProductBrandId == specifiction.BrandId) &&
                   (!specifiction.TypeId.HasValue || x.ProductTypeId == specifiction.TypeId) 
            )
        {
            AddInClude(x => x.ProductBrand);
            AddInClude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPagination(specifiction.PageSize * (specifiction.PageIndex-1) , specifiction.PageSize);

            if (!string.IsNullOrEmpty(specifiction.Sort))
            {
                switch(specifiction.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }

        }

        public ProductsWithTypesAndBrandsSpecifiction(int? id)
            : base(x => x.Id == id )
        {
            AddInClude(x => x.ProductBrand);
            AddInClude(x => x.ProductType);
        }
    }
}
