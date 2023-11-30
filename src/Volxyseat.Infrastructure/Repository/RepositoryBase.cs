using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDataContext _applicationDataContext;
        protected readonly DbSet<TEntity> _entity;
        public IUnitOfWork UnitOfWork => _applicationDataContext;

        public RepositoryBase(ApplicationDataContext applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
            _entity = _applicationDataContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void Dispose()
        {
            _applicationDataContext.Dispose();
        }

        

        public async Task<TEntity> GetById(TKey id)
        {
            return await _entity.FindAsync(id);
        }

       

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return _entity;
        }
        public async Task<int> SaveChangesAsync()
        {
            var result = await _applicationDataContext.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }
    }
}
