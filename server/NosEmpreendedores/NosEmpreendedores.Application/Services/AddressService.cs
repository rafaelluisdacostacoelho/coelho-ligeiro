using NosEmpreendedores.Application.Interfaces;
using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;
using NosEmpreendedores.Domain.Interfaces.Repositories;
using NosEmpreendedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NosEmpreendedores.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public int Create(AddressRequest address)
        {
            return addressRepository.Create(new Address
            {
                Description = address.Description,
                State = address.State,
                City = address.City,
                District = address.District,
                Number = address.Number,
                SupplierId = address.SupplierId
            });
        }

        public IEnumerable<AddressResponse> Read()
        {
            return addressRepository
                .Read()
                .Select(address => new AddressResponse
                {
                    Id = address.Id,
                    Description = address.Description,
                    State = address.State,
                    City = address.City,
                    District = address.District,
                    Number = address.Number,
                    SupplierId = address.SupplierId
                });
        }

        public void Update(Guid id, AddressRequest address)
        {
            addressRepository.Update(new Address
            {
                Id = id,
                Description = address.Description,
                State = address.State,
                City = address.City,
                District = address.District,
                Number = address.Number,
                SupplierId = address.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            addressRepository.Delete(id);
        }

        public AddressResponse GetById(Guid id)
        {
            var address = addressRepository.GetById(id);

            return new AddressResponse
            {
                Id = address.Id,
                Description = address.Description,
                State = address.State,
                City = address.City,
                District = address.District,
                Number = address.Number,
                SupplierId = address.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}