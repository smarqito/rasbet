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
        public async Task<IActionResult> SuspendGame([FromQuery] int gameId, string specialistId)
        {
            try
            {
                await gameOddFacade.SuspendGame(gameId, specialistId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="result"></param>
        /// <param name="specialistId"></param>
        /// <returns></returns>
        [HttpPatch("finish")]
        public async Task<IActionResult> FinishGame([FromQuery] int gameId, string result, string specialistId)
        {
            try
            {
                await gameOddFacade.FinishGame(gameId, result, specialistId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("activate")]
        public async Task<IActionResult> ActivateGame([FromQuery] int gameId, string specialistId)
        {
            try
            {
                await gameOddFacade.ActivateGame(gameId, specialistId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("activeGames")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ActiveGameDTO>))]
        public async Task<IActionResult> GetActivesGames()
        {
            try
            {
                ICollection<ActiveGameDTO> games = await gameOddFacade.GetActiveGames();
                return Ok(games);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("odd")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
        public async Task<IActionResult> GetOdd([FromQuery] int oddId, int betTypeId)
        {
            try
            {
                double d = await gameOddFacade.GetOddValue(oddId, betTypeId);
                return Ok(d);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("odds")]
        public async Task<IActionResult> ChangeOdds([FromBody] ChangeOddsDTO odds)
        {
            try
            {
                await gameOddFacade.ChangeOdds(odds.SpecialistId, odds.BetTypeId, odds.NewOdds);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("GameInfo")]
        public async Task<IActionResult> GetGameInfo([FromQuery] int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
