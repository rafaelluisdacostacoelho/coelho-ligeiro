using System;

namespace NosEmpreendedores.Application.Models.Requests
{
    public class ContactRequest
    {
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}