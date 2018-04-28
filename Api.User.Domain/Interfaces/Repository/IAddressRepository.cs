using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Repository
{
    public interface IAddressRepository
    {
        Task<Entities.Address> Get(int userId);
    }
}
