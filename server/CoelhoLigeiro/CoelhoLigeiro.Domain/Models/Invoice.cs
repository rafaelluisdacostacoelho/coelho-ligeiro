using System;

namespace CoelhoLigeiro.Domain.Models
{
    public class Invoice : Entity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Recurrence { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public int Situation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}