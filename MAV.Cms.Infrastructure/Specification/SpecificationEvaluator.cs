using System.Linq;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MAV.Cms.Infrastructure.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {

        public static IQueryable<TEntity> GetQueryable(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                if (spec.ThenOrderBy != null)
                    query = query.OrderBy(spec.OrderBy).ThenBy(spec.ThenOrderBy);
                else if (spec.ThenOrderByDescending != null)
                    query = query.OrderBy(spec.OrderBy).ThenByDescending(spec.ThenOrderByDescending);
                else
                    query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                if (spec.ThenOrderBy != null)
                    query = query.OrderByDescending(spec.OrderByDescending).ThenBy(spec.ThenOrderBy);
                else if (spec.ThenOrderByDescending != null)
                    query = query.OrderByDescending(spec.OrderByDescending).ThenByDescending(spec.ThenOrderByDescending);
                else
                    query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}