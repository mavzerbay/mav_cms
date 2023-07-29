using MAV.Cms.Common.BaseModels;
using System;
using System.Threading.Tasks;


namespace MAV.Cms.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>();
        Task<int> SaveChangesAsync();
    }
}