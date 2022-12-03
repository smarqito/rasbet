using BetFacade;
using Domain;
using DTO;
using DTO.BetDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetAPI.Controllers;

public class BetController : BaseController
{
    private readonly IBetFacade BetFacade;

    public BetController(IBetFacade betFacade)
    {
        BetFacade = betFacade;
    }

    [HttpPost("simple")]
    //[Authorize(Roles ="AppUser")]
    public async Task<IActionResult> CreateBetSimple([FromBody] CreateSimpleBetDTO create)
    {
        try
        {
            await BetFacade.CreateBetSimple(create.Amount,
                                                            create.UserId,
                                                            create.Selection);
            return Ok();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("multiple")]
    [Authorize(Roles = "AppUser")]
    public async Task<IActionResult> CreateBetMultiple([FromBody] CreateMultipleBetDTO create) 
    {
        try
        {
            BetMultiple bet = await BetFacade.CreateBetMultiple(create.Amount,
                                                        create.UserId,
                                                        create.Selections);
            return Ok(bet);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("open")]
    //[Authorize(Roles = "AppUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetDTO>))]
    public async Task<IActionResult> GetUserBetsOpen([FromQuery] string userId, DateTime start, DateTime end)
    {
        try
        {
            ICollection<BetDTO> bets = await BetFacade.GetUserBetsByState(userId, BetState.Open, start, end);

            return Ok(bets);

        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("won")]
    //[Authorize(Roles = "AppUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetDTO>))]
    public async Task<IActionResult> GetUserBetsWon([FromQuery] string userId, DateTime start, DateTime end)
    {
        try
        {
            ICollection<BetDTO> bets = await BetFacade.GetUserBetsByState(userId, BetState.Won, start, end);

            return Ok(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("closed")]
    //[Authorize(Roles = "AppUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BetDTO>))]
    public async Task<IActionResult> GetUserBetsClosed([FromQuery] string userId, DateTime start, DateTime end) 
    {
        try
        {
            ICollection<BetDTO> closed = await BetFacade.GetUserBetsByStates(userId, BetState.Lost, BetState.Won, start, end);
            return Ok(closed);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBets([FromBody] ICollection<BetsOddsWonDTO> update)
    {
        try
        {
            bool value = await BetFacade.UpdateBets(update);
            return Ok(value);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}