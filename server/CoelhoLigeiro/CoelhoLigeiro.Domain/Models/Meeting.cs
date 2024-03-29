using System;

namespace CoelhoLigeiro.Domain.Models
{
    public class Meeting : Entity
    {
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Local { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}