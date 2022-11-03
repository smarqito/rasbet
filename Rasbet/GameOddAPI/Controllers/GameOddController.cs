using Domain;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using GameOddApplication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameOddAPI.Controllers
{
    public class GameOddController : BaseController
    {
        readonly IGameOddFacade gameOddFacade;

        public GameOddController(IGameOddFacade gameOddFacade)
        {
            this.gameOddFacade = gameOddFacade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialistId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> SuspendGame(string specialistId)
        {
            await gameOddFacade.SuspendGame(specialistId);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="result"></param>
        /// <param name="specialistId"></param>
        /// <returns></returns>
        public async Task<IActionResult> FinishGame(string gameId, string result, string specialistId)
        {
            await gameOddFacade.FinishGame(gameId, result, specialistId);
            return Ok();
        }

        [HttpGet("activeGames")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ActiveGameDTO>))]
        public async Task<IActionResult> GetActivesGames()
        {
            ICollection<ActiveGameDTO> games = await gameOddFacade.GetActiveGames();
            return Ok(games);
        }
    }
}
