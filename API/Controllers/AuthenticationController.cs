using System;
using System.Threading.Tasks;
using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand command)
        {
            try
            {
                var user = await Mediator.Send(command);

                var result = new
                {
                    Token = _tokenService.AppendSecurityToken(),
                    UserID = user.Id,
                    user.UserName,
                    RoleId = (int)user.Role
                };

                return new JsonResult(result);
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

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            return await Task.FromResult(Ok());
        }

    }
}