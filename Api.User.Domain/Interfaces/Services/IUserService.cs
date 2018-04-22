using System.Threading.Tasks;
using Api.User.Domain.Arguments.Response;

namespace Api.User.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<GetUsersByKindOfServiceResponse> GetUsersByKindOfService(string request);
    }
}
