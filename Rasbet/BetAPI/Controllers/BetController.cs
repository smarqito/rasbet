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

    [HttpGet("open")]
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

    [HttpGet("won")]
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

    [HttpGet("lost")]
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

    [HttpGet("amount")]
    public async Task<IActionResult> GetUserBetsByAmount([FromBody] GetUserBetsByValueDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByAmount(get.UserId, get.value);
            return View(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("wonValue")]
    public async Task<IActionResult> GetUserBetsByWonValue([FromBody] GetUserBetsByValueDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByWonValue(get.UserId, get.value);
            return View(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("start")]
    public async Task<IActionResult> GetUserBetsByStart([FromBody] GetUserBetsByDateDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByStart(get.UserId, get.DateTime);
            return View(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("end")]
    public async Task<IActionResult> GetUserBetsByEnd([FromBody] GetUserBetsByDateDTO get)
    {
        try
        {
            ICollection<Bet> bets = await BetFacade.GetUserBetsByStart(get.UserId, get.DateTime);
            return View(bets);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPut("bet")]
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