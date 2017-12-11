using System.Collections.Generic;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Linq;

namespace CoelhoLigeiro.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public int Create(InvoiceRequest invoice)
        {
            return invoiceRepository.Create(new Invoice
            {
                Name = invoice.Name,
                Value = invoice.Value,
                Recurrence = invoice.Recurrence,
                Date = invoice.Date,
                Description = invoice.Description,
                Situation = invoice.Situation,
                CustomerId = invoice.CustomerId,
                SupplierId = invoice.SupplierId
            });
        }

        public IEnumerable<InvoiceResponse> Read()
        {
            return invoiceRepository
                .Read()
                .Select(invoice => new InvoiceResponse
                {
                    Id = invoice.Id,
                    Name = invoice.Name,
                    Value = invoice.Value,
                    Recurrence = invoice.Recurrence,
                    Date = invoice.Date,
                    Description = invoice.Description,
                    Situation = invoice.Situation,
                    CustomerId = invoice.CustomerId,
                    SupplierId = invoice.SupplierId
                });
        }

        public void Update(Guid id, InvoiceRequest invoice)
        {
            invoiceRepository.Update(new Invoice
            {
                Id = id,
                Name = invoice.Name,
                Value = invoice.Value,
                Recurrence = invoice.Recurrence,
                Date = invoice.Date,
                Description = invoice.Description,
                Situation = invoice.Situation,
                CustomerId = invoice.CustomerId,
                SupplierId = invoice.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            invoiceRepository.Delete(id);
        }

        public InvoiceResponse GetById(Guid id)
        {
            var invoice = invoiceRepository.GetById(id);

            return new InvoiceResponse
            {
                Id = invoice.Id,
                Name = invoice.Name,
                Value = invoice.Value,
                Recurrence = invoice.Recurrence,
                Date = invoice.Date,
                Description = invoice.Description,
                Situation = invoice.Situation,
                CustomerId = invoice.CustomerId,
                SupplierId = invoice.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}