using Microsoft.AspNetCore.Mvc;

namespace GameOddAPI.Controllers
{
    public class GameOddController : BaseController
    {
        /// <summary>
        /// Goes to the profs API and create/update the games
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("games")]
        public Task<IActionResult> UpdateGames()
        {
            throw new NotImplementedException();
        }

        [HttpPut("bets")]
        public Task<IActionResult> UpdateBets()
        {
            throw new NotImplementedException();
        }


    }
}
