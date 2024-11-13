using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreAppDb _context;

        public ProductRepository(StoreAppDb context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandAsync()
            => await _context.Set<ProductBrand>().ToListAsync();

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
            => await _context.Set<Product>().ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypeAsync()
            => await _context.Set<ProductType>().ToListAsync();

        public async Task<Product> GetProductByIdAsync(int? id)
            => await _context.Set<Product>().FindAsync(id);
    }
}
