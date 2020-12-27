using System.Threading.Tasks;
using Application.Games.Commands;
using Application.Games.Commands.BuyGame;
using Application.Games.Commands.CheckGame;
using Application.Games.Commands.Comment;
using Application.Games.Commands.CRUDGames;
using Application.Games.Commands.GetGames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("GetGamesByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetGamesByCategory([FromQuery] GetGamesByCategoryCommand command)
        {

            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpGet("GetGamesByGenre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetGamesByGenre([FromQuery] GetGamesByGenreCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpGet("GetRecommendedGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetRecommendedGames([FromQuery] GetRecommendedGamesCommand command)
        {

            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpGet("GetGameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetGameById([FromQuery] GetGameByIdCommand command)
        {

            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("SaveComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> SaveComment([FromBody] SaveCommentCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPut("UpdateComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateComment([FromBody] UpdateCommentCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpDelete("DeleteComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteComment([FromQuery] DeleteCommentCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpGet("CheckGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> CheckGame([FromQuery] CheckGameCommand command)
        {
            var checkGame = await Mediator.Send(command);
            var Result = new
            {
                result = checkGame
            };

            return new JsonResult(Result);
        }

        [Authorize]
        [HttpGet("GetMyGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetUserGames([FromQuery] GetUserGamesCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("BuyGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> BuyGame([FromBody] BuyGameCommand command)
        {
            var checkGame = await Mediator.Send(command);
            var Result = new
            {
                result = checkGame
            };

            return new JsonResult(Result);
        }

        [AllowAnonymous]
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Search([FromQuery] SearchGameCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpGet("GetAllGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Search([FromQuery] GatAllGamesCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("AddNewGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> AddNewGame([FromBody] AddNewGameCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UploadImage([FromForm] UploadGameImageCommand command)
        {
            var commandResult = await Mediator.Send(command);

            var res = new
            {
                result = commandResult
            };

            return new JsonResult(res);
        }

        [Authorize]
        [HttpPost("DeleteGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteGame([FromBody] DeleteGameCommand command)
        {
            var result = await Mediator.Send(command);

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPost("UpdateGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UpdateGame([FromBody] UpdateGameCommand command)
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
