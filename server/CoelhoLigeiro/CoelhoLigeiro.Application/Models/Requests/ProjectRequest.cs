using System;

namespace CoelhoLigeiro.Application.Models.Requests
{
    public class ProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}