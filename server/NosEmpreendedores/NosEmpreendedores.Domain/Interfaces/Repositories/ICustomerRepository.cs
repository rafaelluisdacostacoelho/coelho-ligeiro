using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NosEmpreendedores.Domain.Models;

namespace NosEmpreendedores.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer model);
        Task<IEnumerable<Customer>> ReadAsync(CustomerFilter filter);
        Task UpdateAsync(Customer model);
        Task DeleteAsync(Guid[] guids);
        Task<Customer> GetByIdAsync(Guid guid);
    }
}