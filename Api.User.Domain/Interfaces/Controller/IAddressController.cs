using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.User.Domain.Interfaces.Controller
{
    public interface IAddressController
    {
        Task<IActionResult> Get(int id);
    }
}
