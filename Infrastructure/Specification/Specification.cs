using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; }
        public List<Expression<Func<T, object>>> Includes { get ; }= new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInClude(Expression<Func<T, object>> IncludeExpression)
            => Includes.Add(IncludeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
            => OrderByDescending = orderByDescendingExpression;
        protected void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }
    }
}
