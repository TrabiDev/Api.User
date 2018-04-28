using Api.User.Domain.Arguments.Response;
using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Service
{
    public interface IAddressService
    {
        Task<GetAddressResponse> Get(int idUser);
    }
}
