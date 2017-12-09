using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;

namespace NosEmpreendedores.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierResponse> CreateAsync(SupplierRequest request);
        Task<IEnumerable<SupplierResponse>> ReadAsync(SupplierFilterRequest request);
        Task UpdateAsync(Guid guid, SupplierRequest request);
        Task DeleteAsync(Guid[] guids);
        Task<SupplierResponse> GetByIdAsync(Guid guid);
    }
}