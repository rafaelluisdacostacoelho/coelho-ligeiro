using System;

namespace CoelhoLigeiro.Application.Models.Responses
{
    public class StepResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Responsible { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public DateTime? Creation { get; set; }
        public string Description { get; set; }
        public int Situation { get; set; }
        public Guid ProjectId { get; set; }
    }
}