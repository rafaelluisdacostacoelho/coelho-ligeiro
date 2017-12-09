using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class EventResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Local { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Guid SupplierId { get; set; }
    }
}