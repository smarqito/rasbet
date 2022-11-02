using BetFacade;
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
    public async Task<IActionResult> CreateBetSimple()
    {
        throw new NotImplementedException();
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> CreateBetMultiple() 
    {
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