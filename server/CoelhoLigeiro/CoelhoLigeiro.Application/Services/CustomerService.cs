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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> CreateAsync(CustomerRequest request)
        {
            Customer customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Responsible = request.Responsible,
                Description = request.Description,
                SupplierId = request.SupplierId
            };

            await customerRepository.CreateAsync(customer);

            return new CustomerResponse
            {
                Id = customer.Id
            };
        }

        public async Task<IEnumerable<CustomerResponse>> ReadAsync(CustomerFilterRequest request)
        {
            CustomerFilter filter = new CustomerFilter
            {
                Description = request.Description,
                PersonType = request.PersonType,
                Begin = request.Begin,
                End = request.End
            };

            var customers = await customerRepository.ReadAsync(filter);

            return customers.Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Responsible = customer.Responsible,
                Description = customer.Description,
                Creation = customer.Creation,
                SupplierId = customer.SupplierId
            });
        }

        public async Task UpdateAsync(Guid id, CustomerRequest customer)
        {
            await customerRepository.UpdateAsync(new Customer
            {
                Id = id,
                Name = customer.Name,
                Email = customer.Email,
                Responsible = customer.Responsible,
                Description = customer.Description,
                SupplierId = customer.SupplierId
            });
        }

        public async Task DeleteAsync(Guid[] guids)
        {
            await customerRepository.DeleteAsync(guids);
        }

        public async Task<CustomerResponse> GetByIdAsync(Guid guid)
        {
            var customer = await customerRepository.GetByIdAsync(guid);

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Responsible = customer.Responsible,
                Description = customer.Description,
                Creation = customer.Creation,
                SupplierId = customer.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}