using BetFacade;
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
            var bet = await BetFacade.CreateBetSimple(create.Amount, create.Start, create.UserId, create.SelectionId);
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
        //try
        //{
        //    var bet = await BetFacade.CreateBetMultiple(create.Amount,
        //                                                create.Start,
        //                                                create.OddMultiple,
        //                                                create.SelectionIds);
        //    return View(bet);
        //}
        //catch(Exception e)
        //{
        //    throw new Exception(e.Message);
        //}
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsOpen()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsWon()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserBetsLost() 
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBetsLoss()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBetsWin()
    {
        throw new NotImplementedException();
    }


}