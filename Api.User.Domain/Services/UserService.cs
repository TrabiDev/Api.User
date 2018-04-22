using Api.User.Domain.Arguments.Response;
using Api.User.Domain.Interfaces.Repository;
using Api.User.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Api.User.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUsersByKindOfServiceResponse> GetUsersByKindOfService(string request)
        {
            GetUsersByKindOfServiceResponse response = new GetUsersByKindOfServiceResponse
            {
                Users = await _repository.GetUsersByKindOfService(request)
            };

            return response;
        }

    }
}
