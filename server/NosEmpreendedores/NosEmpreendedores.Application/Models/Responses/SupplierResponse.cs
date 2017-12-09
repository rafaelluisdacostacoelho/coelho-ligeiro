using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class SupplierResponse : EntityResponse
    {
        public string Name { get; set; }
        public string TradingName { get; set; }
        public string CorporateTaxpayerRegistry { get; set; }
        public string Email { get; set; }
        public int Situation { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Creation { get; set; }
        public Guid CategoryId { get; set; }
    }
}