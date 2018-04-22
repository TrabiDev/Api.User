using System.Threading.Tasks;
using Api.User.Domain.Interfaces.Controller;
using Api.User.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.User.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller, IUserController
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _log;

        public UserController(IUserService service, ILogger<UserController> log)
        {
            _log = log;
            _service = service;
        }

        [HttpGet]
        [Route("GetUsersByKindOfService")]
        public async Task<IActionResult> GetUsersByKindOfService(string request)
        {
            try
            {
                var result = await _service.GetUsersByKindOfService(request);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _log.LogError(ex, ex.Message);

                return StatusCode(500, "Internal Error");
            }

        }

    }
}