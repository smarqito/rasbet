using BetAPI.Controllers;
using BetGamesAggregator.Services;
using DTO.BetDTO;
using DTO.BetGamesAggregator;
using DTO.GameOddDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<GameStatisticsDTO>))]
        public async Task<IActionResult> GetActivesGames()
        {
            ICollection<GameDTO> games = await gameOddService.GetActivesGames();
            ICollection<GameStatisticsDTO> res = new List<GameStatisticsDTO>();
            foreach(GameDTO game in games)
            {
                GameStatisticsDTO s = new GameStatisticsDTO(game);
                s.Statistics = await betService.GetStatisticsByGame(game.Id);
                res.Add(s);
            }
            return Ok(res);
        }

        [HttpGet("won")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]
        public async Task<IActionResult> GetUserBetsWon([FromQuery] string userId)
        {
            ICollection<BetDTO> bets = await betService.GetUserBetsWon(userId);
            ICollection<BetGameDTO> res = new List<BetGameDTO>();
            foreach (BetDTO bet in bets)
            {
                BetGameDTO betDTO = new BetGameDTO(bet);
                foreach(SelectionDTO selection in bet.Selections)
                {
                    GameDTO game = await gameOddService.GetGame(selection.GameId);
                    SelectionGameDTO selectionDTO = new SelectionGameDTO(selection.Odd, game, selection.Win);
                    betDTO.Selections.Add(selectionDTO);
                }
                res.Add(betDTO);
            }
            return Ok(res);
        }

        [HttpGet("lost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]

        public async Task<IActionResult> GetUserBetsLost([FromQuery] string userId)
        {
            ICollection<BetDTO> bets = await betService.GetUserBetsLost(userId);
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

        [HttpGet("open")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetGameDTO>))]

        public async Task<IActionResult> GetUserBetsOpen([FromQuery] string userId)
        {
            ICollection<BetDTO> bets = await betService.GetUserBetsOpen(userId);
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

    }
}
