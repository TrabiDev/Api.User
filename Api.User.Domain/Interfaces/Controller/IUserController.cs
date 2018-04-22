using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Controller
{
    public interface IUserController
    {
        Task<IActionResult> GetUsersByKindOfService(string request);
    }
}
