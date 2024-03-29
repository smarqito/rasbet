﻿using Domain;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using GameOddApplication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Specialist")]
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
        //[Authorize(Roles = "Specialist")]
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
        //[Authorize(Roles = "Specialist")]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<CollectiveGameDTO>))]
        public async Task<IActionResult> GetActivesGames()
        {
            try
            {
                ICollection<CollectiveGameDTO> games = await gameOddFacade.GetActiveGames();
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
        //[Authorize(Roles = "Specialist")]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameInfoDTO))]
        public async Task<IActionResult> GetGameInfo([FromQuery] int gameId, bool detailed)
        {
            try
            {
                GameInfoDTO game = await gameOddFacade.GetGameInfo(gameId, detailed);
                return Ok(game);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTO.GameOddDTO.GameDTO))]
        public async Task<IActionResult> GetGame([FromQuery] int gameId)
        {
            try
            {
                DTO.GameOddDTO.GameDTO resp = await gameOddFacade.GetGame(gameId);
                return Ok(resp);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Sports")]
        public async Task<IActionResult> GetAllSports()
        {
            try
            {
                ICollection<SportDTO> resp = await gameOddFacade.GetSports();
                return Ok(resp);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ActiveAndSuspended")]
        public async Task<IActionResult> GetActiveAndSuspendGames()
        {
            try
            {
                ICollection<CollectiveGameDTO> res = await gameOddFacade.GetActiveAndSuspendedGames();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("AddFollowerToGame")]
        public async Task<IActionResult> AddFollowerToGame([FromBody] FollowerDTO dto)
        {
            try
            {
                await gameOddFacade.FollowGame(dto.UserId, dto.GameId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("RemoveFollower")]
        public async Task<IActionResult> RemoveFollower([FromQuery] string userId, int gameId)
        {
            try
            {
                await gameOddFacade.UnfollowGame(userId, gameId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GamesFollowed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<int>))]
        public async Task<IActionResult> GamesFollowed([FromQuery]string userId)
        {
            try
            {
                ICollection<int> res = await gameOddFacade.GetGamesFollowed(userId);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
