using BetFacade;
using Domain;
using DTO;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetAPI.Controllers;

public class BetController : Controller
{
    private readonly IBetFacade BetFacade;

    public BetController(IBetFacade betFacade)
    {
        BetFacade = betFacade;
    }

    [HttpPost("simple")]
    public async Task<IActionResult> CreateBetSimple([FromBody] CreateBetSimpleDTO create )
    {
        try
        {
            BetSimple bet = await BetFacade.CreateBetSimple(create.Amount, create.Start, create.UserId, create.SelectionId);
            return View(bet);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> CreateBetMultiple([FromBody] CreateBetMultipleDTO create) 
    {
        try
        {
            BetMultiple bet = await BetFacade.CreateBetMultiple(create.Amount,
                                                        create.Start,
                                                        create.UserId,
                                                        create.OddMultiple,
                                                        create.SelectionIds);
            return View(bet);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsOpen([FromBody] GetUserBetsDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByState(get.UserId, BetState.Open);

            return View(bets);

        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsWon([FromBody] GetUserBetsDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByState(get.UserId, BetState.Won);

            return View(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsLost([FromBody] GetUserBetsDTO get) 
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByState(get.UserId, BetState.Lost);
            return View(bets);

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
            _=await BetFacade.UpdateBets(update);
            return Ok();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}