using BetApplication.Interfaces;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace BetAPI.Controllers;

public class BetController : Controller
{
    private readonly IBetRepository betRepository;

    public BetController(IBetRepository betRepository)
    {
        this.betRepository = betRepository;
    }

    [HttpPost("simple")]
    public async Task<IActionResult> createBetSimple([FromBody] CreateBetSimpleDTO create)
    {
        await betRepository.CreateBetSimple(create.Amount, 
                                            create.Start, 
                                            create.User, 
                                            create.Selection);

        return Ok();
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> createBetMultiple([FromBody] CreateBetMultipleDTO create) 
    {
        await betRepository.CreateBetMultiple(create.Amount, 
                                              create.Start, 
                                              create.User, 
                                              create.OddMultiple, 
                                              create.Selections);

        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> getUserBetsOpen([FromBody] GetUserBetsDTO get)
    {
        await betRepository.GetUserBetsByState(get.User, Domain.BetState.Open);

        return Ok();    
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> getUserBetsWon([FromBody] GetUserBetsDTO get)
    {
        await betRepository.GetUserBetsByState(get.User, Domain.BetState.Won);

        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> getUserBetsLost([FromBody] GetUserBetsDTO get) 
    {
        await betRepository.GetUserBetsByState(get.User, Domain.BetState.Lost);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> updateBetsLoss([FromBody] UpdateBetsDTO update)
    {
        object value = await betRepository.updateBets((System.Collections.ObjectModel.Collection<int>)update.Bets, Domain.BetState.Lost);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> updateBetsWin([FromBody] UpdateBetsDTO update)
    {
        await betRepository.updateBets((System.Collections.ObjectModel.Collection<int>)update.Bets, Domain.BetState.Lost);

        return Ok();
    }


}