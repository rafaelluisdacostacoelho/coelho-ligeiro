using System;

namespace NosEmpreendedores.Domain.Models
{
    public class Address : Entity
    {
        public string Description { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Number { get; set; }
        public Guid SupplierId { get; set; }
    }
}