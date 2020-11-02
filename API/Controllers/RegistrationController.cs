using System;
using System.Threading.Tasks;
using Application.Registration.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RegistrationController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Registration([FromBody] RegistrationCommand command)
        {
            try
            {
                await Mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                var message = new
                {
                    message = ex.Message
                };
                Response.StatusCode = 401;
                return new JsonResult(message);
            }
        }
    }
}
