using Core.Entities;
using Infrastructure.Specification;
using Services.Helper;
using Services.ProductServices.Dto;

namespace Services.ProductServices
{
    public interface IProductservices
    {
        Task<ProductResultDto> GetProductByIdAsync(int? id);
        Task<Pagination<ProductResultDto>> GetAllProductsAsync(ProductSpecifiction specifiction);
        Task<IReadOnlyList<ProductBrand>> GetAllProductBrandAsync();
        Task<IReadOnlyList<ProductType>> GetAllProductTypeAsync();
    }
}
