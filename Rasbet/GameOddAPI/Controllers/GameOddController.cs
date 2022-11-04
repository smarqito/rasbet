using Domain;
using DTO;
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
        [HttpPatch("suspend")]
        public async Task<IActionResult> SuspendGame([FromQuery] string gameId, string specialistId)
        {
            await gameOddFacade.SuspendGame(gameId, specialistId);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="result"></param>
        /// <param name="specialistId"></param>
        /// <returns></returns>
        [HttpPatch("finish")]
        public async Task<IActionResult> FinishGame([FromQuery] string gameId, string result, string specialistId)
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

        [HttpGet("odd")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
        public async Task<IActionResult> GetOdd([FromQuery] int oddId, int betTypeId)
        {
            double d = await gameOddFacade.GetOddValue(oddId, betTypeId);
            return Ok(d);
        }

        [HttpPatch("odds")]
        public async Task<IActionResult> ChangeOdds([FromBody] ChangeOddsDTO odds)
        {
            await gameOddFacade.ChangeOdds(odds.SpecialistId, odds.BetTypeId, odds.NewOdds);
            return Ok();
        }
    }
}
