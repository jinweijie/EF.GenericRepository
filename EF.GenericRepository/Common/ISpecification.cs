using System;
using System.Linq.Expressions;

namespace EF.GenericRepository.Common
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();

        bool IsSatisfiedBy(T entity);
    }
}
