using System;

namespace NosEmpreendedores.Domain.Models
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}