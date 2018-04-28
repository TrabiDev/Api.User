using System.Threading.Tasks;
using Api.User.Domain.Arguments.Response;

namespace Api.User.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<GetUserResponse> Get(int id = 0, string kindOfService = null);
    }
}
