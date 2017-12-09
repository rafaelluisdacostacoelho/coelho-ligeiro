using System;

namespace NosEmpreendedores.Domain.Models
{
    public class Contact : Entity
    {
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}