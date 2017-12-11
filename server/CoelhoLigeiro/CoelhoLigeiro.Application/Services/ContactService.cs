using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public int Create(ContactRequest contact)
        {
            return contactRepository.Create(new Contact
            {
                Description = contact.Description,
                CustomerId = contact.CustomerId,
                SupplierId = contact.SupplierId
            });
        }

        public IEnumerable<ContactResponse> Read()
        {
            return contactRepository
                .Read()
                .Select(contact => new ContactResponse
                {
                    Id = contact.Id,
                    Description = contact.Description,
                    Creation = contact.Creation,
                    CustomerId = contact.CustomerId,
                    SupplierId = contact.SupplierId
                });
        }

        public void Update(Guid id, ContactRequest contact)
        {
            contactRepository.Update(new Contact
            {
                Id = id,
                Description = contact.Description,
                Creation = contact.Creation,
                CustomerId = contact.CustomerId,
                SupplierId = contact.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            contactRepository.Delete(id);
        }

        public ContactResponse GetById(Guid id)
        {
            Contact contact = contactRepository.GetById(id);

            return new ContactResponse
            {
                Id = contact.Id,
                Description = contact.Description,
                Creation = contact.Creation,
                CustomerId = contact.CustomerId,
                SupplierId = contact.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}