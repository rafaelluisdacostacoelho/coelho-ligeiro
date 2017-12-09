using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class ProjectResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
    }
}