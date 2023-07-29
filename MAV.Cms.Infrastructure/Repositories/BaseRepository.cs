using AutoMapper;
using AutoMapper.QueryableExtensions;
using MAV.Cms.Infrastructure.Specification;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Infrastructure.Data;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MAV.Cms.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MavDbContext _context;
        public BaseRepository(MavDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<R> GetEntityWithSpec<R>(ISpecification<T> spec, IMapper mapper) where R : BaseResponse
        {
            return await ApplySpecification(spec).ProjectTo<R>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<IReadOnlyList<R>> ListWithSpecAsync<R>(ISpecification<T> spec, IMapper mapper) where R : BaseResponse
        {
            return await ApplySpecification(spec).ProjectTo<R>(mapper.ConfigurationProvider).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryable(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
