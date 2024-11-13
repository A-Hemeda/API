using Core.Entities;
using Demo.API.HandleResponse;
using Demo.API.Helper;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Helper;
using Services.ProductServices;
using Services.ProductServices.Dto;

namespace Demo.API.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IProductservices _productServices;

        public ProductsController(IProductservices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResultDto>>> GetProducts([FromQuery]ProductSpecifiction specifiction)
        {
            var product = await _productServices.GetAllProductsAsync(specifiction);
            if (product is null)
                return NotFound(new ApiResponse(400));
            return Ok(product);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [Cache(100)]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int? id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(400));
            return Ok(product);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrand = await _productServices.GetAllProductBrandAsync();
            if (productBrand is null)
                return NotFound(new ApiResponse(400));
            return Ok(productBrand);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productServices.GetAllProductTypeAsync();
            if (productTypes is null)
                return NotFound(new ApiResponse(400));
            return Ok(productTypes);
        }
    }
}
