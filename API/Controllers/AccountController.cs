using System.Threading.Tasks;
using Application.Account.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        [Authorize]
        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UploadImage([FromForm] UploadImageCommand command)
        {
            var commandResult = await Mediator.Send(command);

            var res = new
            {
                result = commandResult
            };

            return new JsonResult(res);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordCommand command)
        {

            var commandResult = await Mediator.Send(command);

            var res = new
            {
                result = commandResult
            };

            return new JsonResult(res);
        }

        [Authorize]
        [HttpPost("DepositAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DepositAccount([FromBody] DepositAccountCommand command)
        {
            var commandResult = await Mediator.Send(command);

            var res = new
            {
                result = commandResult
            };

            return new JsonResult(res);
        }
    }
}
