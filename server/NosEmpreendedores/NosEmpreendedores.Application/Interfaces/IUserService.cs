using NosEmpreendedores.Application.Models.Requests;
using NosEmpreendedores.Application.Models.Responses;

namespace NosEmpreendedores.Application.Interfaces
{
    public interface IUserService : IService<UserResponse, UserRequest>
    {
        
    }
}