using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;

namespace NosEmpreendedores.Application.Interfaces
{
    public interface IContactService : IService<ContactResponse, ContactRequest>
    {
        
    }
}