using System;

namespace NosEmpreendedores.Application.Models.Requests
{
    public class CustomerFilterRequest
    {
        public string Description { get; set; }
        public PersonType PersonType { get; set; }
        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
    }
}