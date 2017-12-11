using System;

namespace CoelhoLigeiro.Domain.Models
{
    public class Step : Entity
    {
        public string Name { get; set; }
        public string Responsible { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public DateTime? Creation { get; set; }
        public int Situation { get; set; }
        public Guid ProjectId { get; set; }
    }
}