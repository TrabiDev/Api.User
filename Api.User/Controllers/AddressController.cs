using System;
using System.Threading.Tasks;
using Api.User.Domain.Arguments.Response;
using Api.User.Domain.Interfaces.Controller;
using Api.User.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.User.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/users/")]
    public class AddressController : BaseController, IAddressController
    {
        private readonly IAddressService _service;
        private readonly ILogger<AddressController> _log;

        public AddressController(IAddressService service, ILogger<AddressController> log) : base(log)
        {
            _log = log;
            _service = service;
        }

        /// <summary>
        /// Busca o endereço de um usuário
        /// </summary>
        /// <remarks>
        /// Possíveis requisições:
        /// 
        /// v1/users/1/address
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <returns>200</returns>
        /// <returns>500</returns>
        [HttpGet]
        [Route("{id}/address")]
        [ProducesResponseType(typeof(GetAddressResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return ResponseExceptionAsync(ex);
            }
        }

    }
}