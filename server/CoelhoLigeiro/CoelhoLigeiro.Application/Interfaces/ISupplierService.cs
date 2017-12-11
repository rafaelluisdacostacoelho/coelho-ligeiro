using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;

namespace CoelhoLigeiro.Application.Interfaces
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