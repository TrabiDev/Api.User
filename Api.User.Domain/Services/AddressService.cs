using Api.User.Domain.Arguments.Response;
using Api.User.Domain.Interfaces.Repository;
using Api.User.Domain.Interfaces.Service;
using System.Threading.Tasks;

namespace Api.User.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;

        public AddressService(IAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAddressResponse> Get(int idUser)
        {
            GetAddressResponse response = new GetAddressResponse
            {
                Address = await _repository.Get(idUser)
            };

            return response;
        }
    }
}
