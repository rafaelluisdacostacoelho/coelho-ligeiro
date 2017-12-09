using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NosEmpreendedores.Application.Interfaces
{
    public interface IService<TEntityResponse, TEntityRequest>
    {
        int Create(TEntityRequest entity);
        IEnumerable<TEntityResponse> Read();
        void Update(Guid id, TEntityRequest entity);
        void Delete(Guid id);
        TEntityResponse GetById(Guid id);
    }

    public interface IServiceAsync<TEntityResponse, TEntityRequest>
    {
        Task<TEntityResponse> CreateAsync(TEntityRequest entity);
        Task<IEnumerable<TEntityResponse>> ReadAsync(TEntityRequest entity);
        Task UpdateAsync(Guid id, TEntityRequest entity);
        Task DeleteAsync(Guid id);
        Task<TEntityResponse> GetByIdAsync(Guid id);
    }
}