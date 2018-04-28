using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Api.User.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _log;

        public BaseController(ILogger<BaseController> log)
        {
            _log = log;
        }

        /// <summary>
        /// Grava o log da exceção e devolve um StatusCode 500
        /// </summary>
        /// <param name="ex">Exception</param>
        protected ObjectResult ResponseExceptionAsync(Exception ex)
        {
            _log.LogError(ex, ex.Message);

            return StatusCode(500, $"Internal Error: {ex.Message}");
        }
    }
}
