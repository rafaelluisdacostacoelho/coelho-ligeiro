using System;

namespace NosEmpreendedores.Application.Models.Requests
{
    public class SupplierFilterRequest
    {
        public string Description { get; set; }
        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
    }
}