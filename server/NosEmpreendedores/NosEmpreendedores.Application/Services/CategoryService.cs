using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosEmpreendedores.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            Category category = new Category { Name = request.Name };

            await categoryRepository.CreateAsync(category);

            return new CategoryResponse
            {
                Id = category.Id
            };
        }

        public async Task<IEnumerable<CategoryResponse>> ReadAsync(string description)
        {
            IEnumerable<Category> categories = await categoryRepository.ReadAsync(description);

            return categories
                .Select(category => new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name
                });
        }

        public async Task UpdateAsync(Guid id, CategoryRequest category)
        {
            await categoryRepository.UpdateAsync(new Category
            {
                Id = id,
                Name = category.Name
            });
        }

        public async Task DeleteAsync(Guid[] ids)
        {
            await categoryRepository.DeleteAsync(ids);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}