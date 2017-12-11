using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoelhoLigeiro.Domain.Models;

namespace CoelhoLigeiro.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task CreateAsync(Supplier model);
        Task<IEnumerable<Supplier>> ReadAsync(SupplierFilter model);
        Task UpdateAsync(Supplier entity);
        Task DeleteAsync(Guid[] guids);
        Task<Supplier> GetByIdAsync(Guid guid);
    }
}