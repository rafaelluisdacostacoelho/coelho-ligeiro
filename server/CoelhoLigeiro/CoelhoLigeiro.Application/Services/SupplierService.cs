using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoelhoLigeiro.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public async Task<SupplierResponse> CreateAsync(SupplierRequest request)
        {
            Supplier supplier = new Supplier
            {
                Name = request.Name,
                TradingName = request.TradingName,
                CorporateTaxpayerRegistry = request.CorporateTaxpayerRegistry,
                Email = request.Email,
                Situation = request.Situation,
                Photo = request.Photo,
                CategoryId = request.CategoryId
            };

            await supplierRepository.CreateAsync(supplier);

            return new SupplierResponse
            {
                Id = supplier.Id
            };
        }

        public async Task<IEnumerable<SupplierResponse>> ReadAsync(SupplierFilterRequest request)
        {
            SupplierFilter filter = new SupplierFilter
            {
                Description = request.Description,
                Begin = request.Begin,
                End = request.End
            };

            var suppliers = await supplierRepository.ReadAsync(filter);

            return suppliers.Select(supplier => new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                TradingName = supplier.TradingName,
                CorporateTaxpayerRegistry = supplier.CorporateTaxpayerRegistry,
                Email = supplier.Email,
                Situation = supplier.Situation,
                Photo = supplier.Photo,
                Creation = supplier.Creation,
                CategoryId = supplier.CategoryId
            });
        }

        public async Task UpdateAsync(Guid guid, SupplierRequest request)
        {
            Supplier supplier = new Supplier
            {
                Id = guid,
                Name = request.Name,
                TradingName = request.TradingName,
                CorporateTaxpayerRegistry = request.CorporateTaxpayerRegistry,
                Email = request.Email,
                Situation = request.Situation,
                Photo = request.Photo,
                CategoryId = request.CategoryId
            };

            await supplierRepository.UpdateAsync(supplier);
        }

        public async Task DeleteAsync(Guid[] guids)
        {
            await supplierRepository.DeleteAsync(guids);
        }

        public async Task<SupplierResponse> GetByIdAsync(Guid guid)
        {
            var supplier = await supplierRepository.GetByIdAsync(guid);

            return new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                TradingName = supplier.TradingName,
                CorporateTaxpayerRegistry = supplier.CorporateTaxpayerRegistry,
                Email = supplier.Email,
                Situation = supplier.Situation,
                Photo = supplier.Photo,
                Creation = supplier.Creation,
                CategoryId = supplier.CategoryId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}