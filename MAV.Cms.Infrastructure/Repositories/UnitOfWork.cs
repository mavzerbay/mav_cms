using MAV.Cms.Common.BaseModels;
using MAV.Cms.Infrastructure.Data;
using MAV.Cms.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace MAV.Cms.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MavDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(MavDbContext context)
        {
            _context = context;

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBaseRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<TEntity>) _repositories[type];
        }
    }
}