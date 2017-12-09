using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NosEmpreendedores.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        int Create(TEntity entity);
        IEnumerable<TEntity> Read();
        void Update(TEntity entity);
        void Delete(Guid id);
        TEntity GetById(Guid id);
    }

    public interface IRepositoryAsync<TEntity> : IDisposable
        where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> ReadAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}