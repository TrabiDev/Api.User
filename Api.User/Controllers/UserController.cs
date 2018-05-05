using System;
using System.Threading.Tasks;
using Api.User.Domain.Arguments.Response;
using Api.User.Domain.Interfaces.Controller;
using Api.User.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.User.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/users")]
    public class UserController : BaseController, IUserController
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _log;

        public UserController(IUserService service, ILogger<UserController> log): base(log)
        {
            _log = log;
            _service = service;
        }

        /// <summary>
        /// Busca os usuários de acordo com os parâmetros recebidos
        /// </summary>
        /// <remarks>
        /// Possíveis requisições:
        /// 
        /// v1/users?id=1
        /// v1/users?kindOfService=Eletricista
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <param name="kindOfService">Tipo de serviço prestado</param>
        /// <returns>200</returns>
        /// <returns>500</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetUserResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Get(int id = 0, string kindOfService = null)
        {
            try
            {
                var result = await _service.Get(id, kindOfService);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {

        }
    }
}