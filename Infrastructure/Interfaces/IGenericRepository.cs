using Core.Entities;
using Infrastructure.Specification;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
