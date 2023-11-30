using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volxyseat.Domain.Core.Data
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(TKey id);
        void Update(TEntity entity);
        Task<IQueryable<TEntity>> GetAll();
        IUnitOfWork UnitOfWork { get; }

    }
}
