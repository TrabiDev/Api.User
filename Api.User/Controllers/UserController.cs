using System.Threading.Tasks;
using Api.User.Domain.Interfaces.Controller;
using Api.User.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.User.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller, IUserController
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetUsersByKindOfService")]
        public async Task<IActionResult> GetUsersByKindOfService(string request)
        {

            var result = await _service.GetUsersByKindOfService(request);

            return Ok(result);

        }

    }
}