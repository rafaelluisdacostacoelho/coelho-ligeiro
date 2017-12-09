using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;

namespace NosEmpreendedores.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<IEnumerable<CategoryResponse>> ReadAsync(string description);
        Task UpdateAsync(Guid guid, CategoryRequest request);
        Task DeleteAsync(Guid[] guids);
    }
}