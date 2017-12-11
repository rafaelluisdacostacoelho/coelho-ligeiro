using System;

namespace CoelhoLigeiro.Domain.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Responsible { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid SupplierId { get; set; }
        public PersonType PersonType { get; set; }
    }
}
