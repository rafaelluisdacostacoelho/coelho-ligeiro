using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;

namespace CoelhoLigeiro.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<IEnumerable<CategoryResponse>> ReadAsync(string description);
        Task UpdateAsync(Guid guid, CategoryRequest request);
        Task DeleteAsync(Guid[] guids);
    }
}