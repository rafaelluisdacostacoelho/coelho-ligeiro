using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class CustomerResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Responsible { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid SupplierId { get; set; }
    }
}