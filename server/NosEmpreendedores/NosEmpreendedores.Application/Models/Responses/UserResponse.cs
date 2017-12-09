using System;

namespace NosEmpreendedores.Application.Models.Responses
{
    public class UserResponse : EntityResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Situation { get; set; }
        public DateTime Creation { get; set; }
        public Guid SupplierId { get; set; }
    }
}