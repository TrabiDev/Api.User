using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<List<Entities.User>> GetUsersByKindOfService(string kindOfService);
    }
}
