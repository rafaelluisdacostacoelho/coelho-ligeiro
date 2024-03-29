using CoelhoLigeiro.Domain.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoelhoLigeiro.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category model);
        Task<IEnumerable<Category>> ReadAsync(string description);
        Task UpdateAsync(Category model);
        Task DeleteAsync(Guid[] guids);
    }
}