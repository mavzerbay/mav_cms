using AutoMapper;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAV.Cms.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<R> GetEntityWithSpec<R>(ISpecification<T> spec, IMapper mapper) where R : BaseResponse;
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<R>> ListWithSpecAsync<R>(ISpecification<T> spec, IMapper mapper) where R : BaseResponse;
        Task<int> CountAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
