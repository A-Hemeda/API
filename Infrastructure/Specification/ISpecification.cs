using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get;}
        public Expression<Func<T, object>> OrderBy { get;}
        public Expression<Func<T, object>> OrderByDescending { get;}
        public List<Expression<Func<T, object>>> Includes { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPaginated { get; }
    }
}
