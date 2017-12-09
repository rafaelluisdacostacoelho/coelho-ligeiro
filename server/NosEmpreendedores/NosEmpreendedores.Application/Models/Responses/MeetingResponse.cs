using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class MeetingResponse : EntityResponse
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