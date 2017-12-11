using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;

namespace CoelhoLigeiro.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateAsync(CustomerRequest request);
        Task<IEnumerable<CustomerResponse>> ReadAsync(CustomerFilterRequest request);
        Task UpdateAsync(Guid id, CustomerRequest request);
        Task DeleteAsync(Guid[] ids);
        Task<CustomerResponse> GetByIdAsync(Guid identifier);
    }
}