using System;

namespace CoelhoLigeiro.Domain.Models
{
    public class Event : Entity
    {
        public string Name { get; set; }
        public string Local { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Guid SupplierId { get; set; }
    }
}