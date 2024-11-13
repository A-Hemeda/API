using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreAppDb _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreAppDb context)
        {
            _context = context;
        }

        public async Task<int> Compelete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null )
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name; // the entity name 

            if (!_repositories.ContainsKey(type))
            {
                var reopsitoryType = typeof(GenericRepository<>); 
                var repositoryInstanse = Activator.CreateInstance(reopsitoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstanse);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
