using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;

namespace CoelhoLigeiro.Application.Interfaces
{
    public interface IContactService : IService<ContactResponse, ContactRequest>
    {
        
    }
}