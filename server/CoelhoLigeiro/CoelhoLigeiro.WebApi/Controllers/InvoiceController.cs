using Microsoft.AspNetCore.Mvc;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using System;
using System.Collections.Generic;

namespace CoelhoLigeiro.WebApi.Controllers
{
    [Route("api/invoices")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpPost]
        public void Create([FromBody]InvoiceRequest model)
        {
            invoiceService.Create(model);
        }

        [HttpGet]
        public IEnumerable<InvoiceResponse> Read()
        {
            return invoiceService.Read();
        }

        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]InvoiceRequest model)
        {
            invoiceService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            invoiceService.Delete(id);
        }

        [HttpGet("{id}")]
        public InvoiceResponse GetById(Guid id)
        {
            return invoiceService.GetById(id);
        }
    }
}