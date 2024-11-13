using AutoMapper;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specification;
using Services.Helper;
using Services.ProductServices.Dto;

namespace Services.ProductServices
{
    public class Productservices : IProductservices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Productservices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandAsync()
            => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<Pagination<ProductResultDto>> GetAllProductsAsync(ProductSpecifiction specifiction)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifiction(specifiction);
            var product = await _unitOfWork.Repository<Product>().GetAllWithSpecificationAsync(spec);
            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(spec);
            var mappedProduct = _mapper.Map<IReadOnlyList<ProductResultDto>>(product);
            //return mappedProduct;
            return new Pagination<ProductResultDto>(specifiction.PageIndex , specifiction.PageSize , totalItems , mappedProduct);
        }


        public async Task<IReadOnlyList<ProductType>> GetAllProductTypeAsync()
            => await _unitOfWork.Repository<ProductType>().GetAllAsync();

        public async Task<ProductResultDto> GetProductByIdAsync(int? id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifiction(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecificationAsync(spec);
            var mappedProduct = _mapper.Map<ProductResultDto>(product);
            return mappedProduct;
        }
    }
}
