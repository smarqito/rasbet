using BetAPI.Controllers;
using BetGamesAggregator.Services;
using DTO.BetDTO;
using DTO.BetGamesAggregator;
using DTO.GameOddDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;

namespace BetGamesAggregator.Controllers
{
    public class BetGamesController : BaseController
    {
        private readonly IBetService betService;
        private readonly IGameOddService gameOddService;

        public BetGamesController(IBetService betService, IGameOddService gameOddService)
        {
            this.betService = betService;
            this.gameOddService = gameOddService;
        }

        [HttpGet("activeGames")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<GameStatisticsDTO>))]
        public async Task<IActionResult> GetActivesGames()
        {
            try
            {

                ICollection<CollectiveGameDTO> games = await gameOddService.GetActivesGames();
                ICollection<GameStatisticsDTO> res = new List<GameStatisticsDTO>();
                foreach (CollectiveGameDTO game in games)
                {
                    GameStatisticsDTO s = new GameStatisticsDTO(game.Id, game.StartTime, game.SportName, game.MainBet);
                    s.Statistics = await betService.GetStatisticsByGame(game.MainBet.Odds.Select(x => x.Id).ToList());
                    res.Add(s);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("won")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]
        public async Task<IActionResult> GetUserBetsWon([FromQuery] string userId, DateTime start, DateTime end)
        {
            try
            {
                if (Request.Headers.TryGetValue("Authorization", out var header))
                {
                    ICollection<BetDTO> bets = await betService.GetUserBetsWon(userId, start, end, header);
                    ICollection<BetGameDTO> res = new List<BetGameDTO>();
                    foreach (BetDTO bet in bets)
                    {
                        BetGameDTO betDTO = new BetGameDTO(bet);
                        foreach (SelectionDTO selection in bet.Selections)
                        {
                            GameDTO game = await gameOddService.GetGame(selection.GameId);
                            SelectionGameDTO selectionDTO = new SelectionGameDTO(selection.Odd, game, selection.Win);
                            betDTO.Selections.Add(selectionDTO);
                        }
                        res.Add(betDTO);
                    }
                    return Ok(res);
                }
                else
                {
                    throw new Exception("No permitions");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("closed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]

        public async Task<IActionResult> GetUserBetsClosed([FromQuery] string userId, DateTime start, DateTime end)
        {
            try
            {
                if (Request.Headers.TryGetValue("Authorization", out var header))
                {
                    ICollection<BetDTO> bets = await betService.GetUserBetsClosed(userId, start, end, header);
                    ICollection<BetGameDTO> res = new List<BetGameDTO>();
                    foreach (BetDTO bet in bets)
                    {
                        BetGameDTO betDTO = new BetGameDTO(bet);
                        foreach (SelectionDTO selection in bet.Selections)
                        {
                            GameDTO game = await gameOddService.GetGame(selection.GameId);
                            SelectionGameDTO selectionDTO = new SelectionGameDTO(selection.Odd, game, selection.Win);
                            betDTO.Selections.Add(selectionDTO);
                        }
                        res.Add(betDTO);
                    }
                    return Ok(res);
                }
                else
                {
                    throw new Exception("No permitions");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("open")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]

        public async Task<IActionResult> GetUserBetsOpen([FromQuery] string userId, DateTime start, DateTime end)
        {
            try
            {
                if (Request.Headers.TryGetValue("Authorization", out var header))
                {
                    ICollection<BetDTO> bets = await betService.GetUserBetsOpen(userId, start, end, header);
                    ICollection<BetGameDTO> res = new List<BetGameDTO>();
                    foreach (BetDTO bet in bets)
                    {
                        BetGameDTO betDTO = new BetGameDTO(bet);
                        foreach (SelectionDTO selection in bet.Selections)
                        {
                            GameDTO game = await gameOddService.GetGame(selection.GameId);
                            SelectionGameDTO selectionDTO = new SelectionGameDTO(selection.Odd, game, selection.Win);
                            betDTO.Selections.Add(selectionDTO);
                        }
                        res.Add(betDTO);
                    }
                    return Ok(res);
                }
                else
                {
                    throw new Exception("No permitions");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
