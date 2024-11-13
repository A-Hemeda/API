using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreAppDb _context;

        public GenericRepository(StoreAppDb context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int? id)
            => await _context.Set<T>().FindAsync(id);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public async Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification)
            => await ApplySpecification(specification).FirstOrDefaultAsync();



        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
            => await ApplySpecification(specification).ToListAsync();

        public async Task<int> CountAsync(ISpecification<T> specification)
            => await ApplySpecification(specification).CountAsync();
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
            => SpecificationEvaluator<T>.MakeQuery(_context.Set<T>().AsQueryable(), specification);

        
    }
}
