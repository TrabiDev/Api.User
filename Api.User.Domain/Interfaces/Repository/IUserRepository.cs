using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Entities.User>> GetUsersByKindOfService(string kindOfService);
    }
}
