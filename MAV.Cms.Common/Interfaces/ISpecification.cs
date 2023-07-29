using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MAV.Cms.Common.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> ThenOrderBy { get; }
        Expression<Func<T, object>> ThenOrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}