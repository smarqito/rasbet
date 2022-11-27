using BetGamesAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Controllers
{
    public class BetGamesController : Controller
    {
        private readonly IBetService betService;
        private readonly IGameOddService gameOddService;

        public BetGamesController(IBetService betService, IGameOddService gameOddService)
        {
            this.betService = betService;
            this.gameOddService = gameOddService;
        }

        [HttpGet("won")]
        public Task<IActionResult> GetUserBetsWon([FromQuery] string userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("lost")]
        public Task<IActionResult> GetUserBetsLost([FromQuery] string userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("open")]
        public Task<IActionResult> GetUserBetsOpen([FromQuery] string userId)
        {
            throw new NotImplementedException();
        }

    }
}
